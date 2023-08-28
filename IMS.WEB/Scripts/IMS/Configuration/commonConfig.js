var currentAction = "";
$(function () {
  // AJAX call to fetch VendorType data
  renderEjGrid(FETCH_URL);
});
//#region  ROW BOUND
function rowBound(args) {
  if (args.data["Status"] == 1) {
    args.row[0].cells[3].innerText = "Active";
  } else {
    args.row[0].cells[3].innerText = "Inactive";
  }
  if (args.data["CreationDate"] != null && args.row[0].cells[5]) {
    var jsonDate = args.row[0].cells[5].innerText;
    var dateObject = parseJsonDate(jsonDate);
    args.row[0].cells[5].innerText = dateObject;
    // console.log(dateObject);
  }
}
//#endregion
//#region ADD BUTTON ACTION
function addButtonAction(args) {
  console.log("hello", args.model);
}
//#endregion
//#region DELETE BUTTON ACTION
function deleteButtonAction(args) {
  args.cancel = true;
  // console.log(args);
  let id = args.data.Id;
  $.ajax({
    url: DELETE_URL + id, // Replace with the actual URL for your delete action
    method: "POST",
    success: function (data) {
      // Handle the success response from the server
      console.log("Record deleted successfully:", data);

      renderEjGrid(FETCH_URL);
    },
    error: function (error) {
      // Handle the error response from the server
      console.error("Error deleting record:", error);
    },
  });
}
//#endregion
//#region SAVE BUTTON ACTION
function saveButtonAction(args) {
  // args.cancel = true;
  if (args.requestType === "save" && currentAction == "create") {
    var formData = $("#GridEditForm").serialize();
    var formDataObject = parseQueryString(formData);
    // Make an AJAX POST request to the create action
    $.ajax({
      url: CREATE_URL, // Replace with the actual URL for your create action
      method: "POST",
      data: formDataObject,
      success: function (data) {
        renderEjGrid(FETCH_URL);
        currentAction = "";
        console.log("create Succesful", data);
      },
      error: function (error) {
        // Handle the error response from the server
        console.error("Error in create action:", error);
      },
    });
  }
  if (args.requestType === "save" && currentAction == "edit") {
    var formData = $("#GridEditForm").serialize();
    var formDataObject = parseQueryString(formData);
    formDataObject.Id = args.rowData.Id;
    // Make an AJAX POST request to the create action
    $.ajax({
      url: EDIT_URL, // Replace with the actual URL for your create action
      method: "POST",
      data: formDataObject,
      success: function (data) {
        renderEjGrid(FETCH_URL);
        currentAction = "";
      },
      error: function (error) {
        // Handle the error response from the server
        console.error("Error in edit action:", error);
      },
    });
  }
}
//#endregion
//#region CANCEL BUTTON ACTION
function cancelButtonAction(args) {
  args.cancel = true;
  currentAction = "";
}
//#endregion
//#region COMPLETE(args)
function complete(args) {
  //   console.log(this);
  if (args.requestType == "beginedit" || args.requestType == "add") {
    if (args.requestType == "add") {
      console.log("Add Action Triggered", this);
      $("#" + this._id + "_dialogEdit").ejDialog({
        title: "Add New  Type",
      });
      $("#GridStatus").parent().parent().parent().parent().hide();
      $("#GridCreatedBy").parent().parent().hide();
      $("#GridCreationDate").parent().parent().hide();
      $("#GridRank").parent().parent().parent().parent().hide();
      currentAction = "create";
    } else {
      $("#" + this._id + "_dialogEdit").ejDialog({
        title: "Edit  Type",
      });
      // console.log("Edit Action Triggered", args);
      $("#GridStatus").parent().parent().parent().parent().hide();
      $("#GridStatus").parent().parent().parent().hide();
      $("#GridCreationDate").parent().parent().hide();
      $("#GridRank").parent().parent().parent().parent().hide();

      currentAction = "edit";
    }
  }
}
//#endregion

//#region RENDER EJ GRID FUNCTION
function renderEjGrid(api) {
  $.ajax({
    url: api,
    method: "GET",
    dataType: "json",
    success: function (data) {
      // Initialize EJ2 Grid with the fetched CustomerType data
      console.log("received data from the server,  types : ", data);

      //sort the data accroding to their rank

      $("#Grid").ejGrid({
        dataSource: ej.DataManager({
          json: sortByRank(data), // Use the fetched CustomerType data
          adaptor: new ej.remoteSaveAdaptor(),
          insertUrl: "", // Specify the insert URL
          updateUrl: "", // Specify the update URL
          removeUrl: "", // Specify the remove URL
        }),
        sortSettings: {
          sortedColumns: [{ field: "Rank", direction: "Ascending" }],
        },
        loadingIndicator: { indicatorType: "Shimmer" },
        toolbarSettings: {
          showToolbar: true,
          toolbarItems: [
            "add",
            "edit",
            "delete",
            "cancel",
            "search",
            "printGrid",
          ],
        },
        editSettings: {
          allowEditing: true,
          allowAdding: true,
          allowDeleting: true,
          showDeleteConfirmDialog: true,
          showAddNewDialog: true,
          editMode: "dialog",
        },
        beginEdit: function (args) {
          console.log("beginEdit", args);
        },

        isResponsive: true,
        enableResponsiveRow: true,
        allowSorting: true,
        allowMultiSorting: false,
        allowSearching: true,
        allowFiltering: true,
        filterSettings: {
          filterType: "excel",
          maxFilterChoices: 100,
          enableCaseSensitivity: false,
        },
        allowPaging: true,
        pageSettings: {
          pageSize: 10,
          printMode: ej.Grid.PrintMode.CurrentPage,
        },
        columns: [
          {
            field: "Id",
            headerText: " Type Id",
            isPrimaryKey: true,
            isIdentity: true,
            visible: false,
          },
          {
            field: "Name",
            headerText: " Type Name",
            validationRules: { required: true },
          },
          {
            field: "Description",
            headerText: "Description",
            validationRules: {
              required: true,
              //customRule: function (value, column, record) {
              //    // Define your custom validation logic here
              //    // For example, let's say you want the description to be at least 5 characters long
              //    console.log(value);
              //},
              //customMessage: "Description must be at least 5 characters long.",
            },
            allowSorting: false,
          },
          {
            field: "Status",
            headerText: "Status",
            allowSorting: false,
            editType: "numericedit",
            defaultValue: 1,
          },
          {
            field: "Rank",
            headerText: "Rank",
            editType: "numericedit",
            defaultValue: 1,
            visible: false,
          },
          // {
          //   field: "CreatedBy",
          //   headerText: "CreatedBy",
          //   allowSorting: false,
          // },
          { field: "CreationDate", headerText: "Creation Date" },
        ],
        actionComplete: "complete",
        actionBegin: function (args) {
          switch (args.requestType) {
            case "save":
              saveButtonAction(args);
              break;
            case "delete":
              deleteButtonAction(args);
              break;
            case "add":
              addButtonAction(args);
              break;
            case "edit":
              editButtonAction(args);
              break;
            case "cancel":
              cancelButtonAction(args);
              break;
            case "refresh":
              handleRefresh(args);
              break;
          }
        },
        rowDataBound: rowBound,
      });
    },
    error: function (error) {
      console.error("Error fetching  types:", error);
    },
  });
}
//#endregion
//#region HANDLE REFRESH
function handleRefresh(args) {
  if (args.requestType === "refresh" && currentAction == "edit") {
    console.log("refresh Called");
    args.cancel = true;
  }
}
//#endregion

//#region PARSE JSON DATE
function parseJsonDate(jsonDate) {
  // Extract the timestamp from the JSON date format
  var timestamp = parseInt(jsonDate.substr(6));
  var currentDate = new Date(timestamp);
  // Create a new Date object using the timestamp
  const formattedDate = formatDate(currentDate);

  return formattedDate;
}
//#endregion
//#region FORMATDATE
function formatDate(date) {
  const months = [
    "Jan",
    "Feb",
    "Mar",
    "Apr",
    "May",
    "Jun",
    "Jul",
    "Aug",
    "Sep",
    "Oct",
    "Nov",
    "Dec",
  ];

  const day = date.getDate().toString().padStart(2, "0");
  const month = months[date.getMonth()];
  const year = date.getFullYear().toString().slice(-2);

  return `${day}-${month}-${year}`;
}
//#endregion
//#region SORT BY RANK
function sortByRank(data) {
  return data.sort((a, b) => a.Rank - b.Rank);
}
//#endregion

//#region PARSE QUERY STRING
function parseQueryString(queryString) {
  var params = {};
  var pairs = queryString.split("&");

  for (var i = 0; i < pairs.length; i++) {
    var pair = pairs[i].split("=");
    var key = decodeURIComponent(pair[0]);
    var value = decodeURIComponent(pair[1]);
    params[key] = value;
  }

  return params;
}
//#endregion

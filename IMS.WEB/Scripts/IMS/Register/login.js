$(document).ready(function () {
  let validator = $("#loginForm").validate({
    invalidHandler: function (event, validator) {
      var errors = validator.numberOfInvalids();
      if (errors) {
        console.log(validator);
        console.log("number of errors : ", errors);
      }
    },
    rules: {
      Email: {
        required: true,
        email: true,
      },
      Password: {
        required: true,
        minlength: 6,
      },
    },
    messages: {
      Email: {
        required: "Please enter your email address",
        email: "Please enter a valid email address",
      },
      Password: {
        required: "Please enter a password",
        minlength: "Password must be at least 6 characters long",
      },
    },
  });

  $("#loginForm").on("submit", function (e) {
    e.preventDefault();
    if (validator.form()) {
      let emailInput = $("#email").val();
      let passwordInput = $("#password").val();
      let rememberMeInput = true;

      let data = {
        Email: emailInput,
        Password: passwordInput,
        RememberMe: rememberMeInput,
      };

      $.ajax({
        url: "/account/login", // Replace with your actual endpoint
        type: "POST",
        data: data,
        success: function (response) {
          if (response.success) {
            console.log("Login successful", response);
            let returnUrl = extractReturnUrlFromQueryString();
            window.location.href = returnUrl;
          } else {
            console.log("Login failed", response.errors);
          }
        },
        error: function (error) {
          console.error("Login failed", error);
        },
      });
    } else {
      console.log("form not valid");
    }
  });

  $("#registerBtn").click(function () {
    window.location.href = "/Account/Register";
  });
});

function extractReturnUrlFromQueryString() {
  // Get the query parameters from the URL
  var queryParams = window.location.search.substring(1);

  // Split the parameters into an array
  var paramPairs = queryParams.split("&");

  // Search for the ReturnUrl parameter
  for (var i = 0; i < paramPairs.length; i++) {
    var paramPair = paramPairs[i].split("=");
    if (paramPair[0] === "ReturnUrl") {
      // Decode and return the parameter value
      return decodeURIComponent(paramPair[1]);
    }
  }

  // Return null if ReturnUrl parameter is not found
  return "/Home/Index";
}

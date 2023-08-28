/******/ (() => { // webpackBootstrap
/******/ 	"use strict";
/******/ 	var __webpack_modules__ = ({

/***/ "jquery":
/*!*************************!*\
  !*** external "jQuery" ***!
  \*************************/
/***/ ((module) => {

module.exports = jQuery;

/***/ }),

/***/ "jquery.validation":
/*!***************************************!*\
  !*** external "jQuery.fn.validation" ***!
  \***************************************/
/***/ ((module) => {

module.exports = jQuery.fn.validation;

/***/ })

/******/ 	});
/************************************************************************/
/******/ 	// The module cache
/******/ 	var __webpack_module_cache__ = {};
/******/ 	
/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {
/******/ 		// Check if module is in cache
/******/ 		var cachedModule = __webpack_module_cache__[moduleId];
/******/ 		if (cachedModule !== undefined) {
/******/ 			return cachedModule.exports;
/******/ 		}
/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = __webpack_module_cache__[moduleId] = {
/******/ 			// no module.id needed
/******/ 			// no module.loaded needed
/******/ 			exports: {}
/******/ 		};
/******/ 	
/******/ 		// Execute the module function
/******/ 		__webpack_modules__[moduleId](module, module.exports, __webpack_require__);
/******/ 	
/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}
/******/ 	
/************************************************************************/
/******/ 	/* webpack/runtime/compat get default export */
/******/ 	(() => {
/******/ 		// getDefaultExport function for compatibility with non-harmony modules
/******/ 		__webpack_require__.n = (module) => {
/******/ 			var getter = module && module.__esModule ?
/******/ 				() => (module['default']) :
/******/ 				() => (module);
/******/ 			__webpack_require__.d(getter, { a: getter });
/******/ 			return getter;
/******/ 		};
/******/ 	})();
/******/ 	
/******/ 	/* webpack/runtime/define property getters */
/******/ 	(() => {
/******/ 		// define getter functions for harmony exports
/******/ 		__webpack_require__.d = (exports, definition) => {
/******/ 			for(var key in definition) {
/******/ 				if(__webpack_require__.o(definition, key) && !__webpack_require__.o(exports, key)) {
/******/ 					Object.defineProperty(exports, key, { enumerable: true, get: definition[key] });
/******/ 				}
/******/ 			}
/******/ 		};
/******/ 	})();
/******/ 	
/******/ 	/* webpack/runtime/hasOwnProperty shorthand */
/******/ 	(() => {
/******/ 		__webpack_require__.o = (obj, prop) => (Object.prototype.hasOwnProperty.call(obj, prop))
/******/ 	})();
/******/ 	
/******/ 	/* webpack/runtime/make namespace object */
/******/ 	(() => {
/******/ 		// define __esModule on exports
/******/ 		__webpack_require__.r = (exports) => {
/******/ 			if(typeof Symbol !== 'undefined' && Symbol.toStringTag) {
/******/ 				Object.defineProperty(exports, Symbol.toStringTag, { value: 'Module' });
/******/ 			}
/******/ 			Object.defineProperty(exports, '__esModule', { value: true });
/******/ 		};
/******/ 	})();
/******/ 	
/************************************************************************/
var __webpack_exports__ = {};
// This entry need to be wrapped in an IIFE because it need to be isolated against other modules in the chunk.
(() => {
/*!*********************!*\
  !*** ./dist/app.js ***!
  \*********************/
__webpack_require__.r(__webpack_exports__);
/* harmony import */ var jquery__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! jquery */ "jquery");
/* harmony import */ var jquery__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(jquery__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var jquery_validation__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! jquery.validation */ "jquery.validation");
/* harmony import */ var jquery_validation__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(jquery_validation__WEBPACK_IMPORTED_MODULE_1__);


// registerLicense(
//   "Ngo9BigBOggjHTQxAR8/V1NGaF1cVWhIfEx1RHxQdld5ZFRHallYTnNWUj0eQnxTdEZjUX5ecHNVRWNVVUV1Xw=="
// );
jquery__WEBPACK_IMPORTED_MODULE_0___default()(document).ready(function () {
    jquery__WEBPACK_IMPORTED_MODULE_0___default()("#registrationForm").submit(function (event) {
        event.preventDefault(); // Prevent the default form submission
        // Your custom code here, such as validation
        // If you decide to submit the form after custom actions, you can do so programmatically:
        // $(this).submit();
    });
    // Custom validation method to check for numbers in the value
    // $.validator.addMethod(
    //   "noNumbers",
    //   function (value, element) {
    //     return this.optional(element) || !/\d/.test(value);
    //   },
    //   "Numbers are not allowed in the first name."
    // );
    console.log(jquery__WEBPACK_IMPORTED_MODULE_0___default()("#registrationForm"));
    jquery__WEBPACK_IMPORTED_MODULE_0___default()("#registrationForm").validate({
        debug: true,
        rules: {
            // "first-name": { required: true, noNumbers: true },
            // "last-name": "required",
            email: {
                required: true,
                email: true,
            },
            password: {
                required: true,
                minlength: 6,
            },
        },
        messages: {
            // "first-name": {
            //   required: "Please enter your first name",
            //   noNumbers: "Numbers are not allowed in the first name.",
            // },
            // "last-name": "Please enter your last name",
            email: {
                required: "Please enter your email address",
                email: "Please enter a valid email address",
            },
            password: {
                required: "Please enter a password",
                minlength: "Password must be at least 6 characters long",
            },
        },
        submitHandler: function (e) {
            e.preventDefault();
            // Do something with the form data (e.g., send to server)
            alert("Form submitted successfully");
        },
    });
});

})();

/******/ })()
;
//# sourceMappingURL=musarrat.js.map
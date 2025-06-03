$(document).ready(function () {
  const loginForm = $("#login");
  const signupForm = $("#signup");
  const signUpBtn = $("#login .toggle");
  const loginBtn = $("#signup .toggle");

  signUpBtn.on("click", function (e) {
    e.preventDefault();

    loginForm.animate({ left: "-100%" }, 200, function () {
      signupForm
        .css({ display: "block", left: "100%" })
        .animate({ left: "0" }, 200);
      setTimeout(() => {
        loginForm.css("display", "none");
      }, (timeout = 200));
    });
  });

  loginBtn.on("click", function (e) {
    e.preventDefault();

    signupForm.animate({ left: "100%" }, 200, function () {
      loginForm
        .css({ display: "block", left: "-100%" })
        .animate({ left: "0" }, 200);
      setTimeout(() => {
        signupForm.css("display", "none");
      }, (timeout = 200));
    });
  });
});

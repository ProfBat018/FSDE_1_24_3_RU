const loginTab = document.getElementById("loginTab");
const registerTab = document.getElementById("registerTab");
const formSlider = document.getElementById("formSlider");

loginTab.addEventListener("click", () => {
  formSlider.style.transform = "translateX(0%)";
  loginTab.classList.add("bg-blue-500", "text-white");
  loginTab.classList.remove("bg-gray-100", "text-blue-500");
  registerTab.classList.add("bg-gray-100", "text-blue-500");
  registerTab.classList.remove("bg-blue-500", "text-white");
});

registerTab.addEventListener("click", () => {
  formSlider.style.transform = "translateX(-50%)";
  registerTab.classList.add("bg-blue-500", "text-white");
  registerTab.classList.remove("bg-gray-100", "text-blue-500");
  loginTab.classList.add("bg-gray-100", "text-blue-500");
  loginTab.classList.remove("bg-blue-500", "text-white");
});

const loginForm = document.querySelector("#loginForm");
const registerForm = document.querySelector("#registerForm");

loginForm.addEventListener("submit", (e) => {
  e.preventDefault(); // Предотвращаем стандартное поведение формы при отправке

  const username = document.querySelector("#usernameInput").value;
  const password = document.querySelector("#passwordInput").value;

  const loginData = {
    username,
    password,
  };

  // Example 1

  /*
  const request = new XMLHttpRequest();

  request.open("POST", "http://localhost:5001/api/v1/Auth/Login", true);
  request.setRequestHeader("Content-Type", "application/json");

  request.send(JSON.stringify(loginData));

  request.addEventListener("load", () => {
    if (request.status >= 200 && request.status < 300) {
      console.log("Login successful:", request.responseText);
    } else {
      console.error("Login failed:", request.statusText);
    }
  });
  */

  // Example 2

  /*
  const request = fetch("http://localhost:5001/api/v1/Auth/Login", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(loginData),
  });

  request
    .then((response) => {
      if (!response.ok) {
        throw new Error("Network response was not ok " + response.statusText);
      }
      return response.json(); // json тоже промис
    })
    .then((data) => {
      console.log("Login successful:", data);
    })
    .catch((error) => {
      console.error("Login failed:", error);
    });
*/

  // Example 3

  const loginCall = async () => {
    const request = await fetch("http://localhost:5001/api/v1/Auth/Login", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(loginData),
    });

    if (!request.ok) {
      throw new Error("Network response was not ok " + request.statusText);
    }
    const data = await request.json(); // json тоже промис
    console.log("Login successful:", data);
  };

  loginCall()
    .then(() => {
      console.log("Login successful");
    })
    .catch((error) => {
      console.error("Login failed:", error);
    });

});

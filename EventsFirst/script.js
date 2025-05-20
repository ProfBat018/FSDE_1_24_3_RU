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

const loginForm = document.getElementById("loginForm");
const registerForm = document.getElementById("registerForm");

loginForm.addEventListener("submit", (e) => {
    e.preventDefault();
    const email = document.getElementById("loginEmail").value;
    const password = document.getElementById("loginPassword").value;
    
    

    console.log("Login:", { email, password });
    });


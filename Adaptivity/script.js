function toggleTheme() {
  document.body.classList.toggle("dark-theme");
  const info = document.getElementById("info");
  info.textContent =
    window.innerWidth <= 600
      ? "Мобильная тема переключена"
      : "Десктопная тема переключена";
}

window.addEventListener("resize", () => {
  const info = document.getElementById("info");
  if (window.innerWidth <= 600) {
    info.textContent = "Вы используете мобильное устройство";
  } else {
    info.textContent = "Вы используете десктоп";
  }
});

const style = document.createElement("style");
style.innerHTML = `
  .dark-theme {
    background-color: #222;
    color: #fff;
  }
`;
document.head.appendChild(style);

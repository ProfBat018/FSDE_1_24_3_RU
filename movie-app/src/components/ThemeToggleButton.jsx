import { useContext } from "react";
import { ThemeContext } from "../contexts/ThemeContext";


export const ThemeToggleButton = () => {
  const { theme, toggleTheme } = useContext(ThemeContext);

  return (
    <button
    onClick={toggleTheme}
    className={`px-4 py-2 rounded border 
      ${theme === "dark" ? "bg-black text-white border-white" : "bg-white text-black border-black"}`}
  >
    {theme === "dark" ? "Switch to Light" : "Switch to Dark"}
  </button>
  );
  
};
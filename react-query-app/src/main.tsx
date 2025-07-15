import { createRoot } from "react-dom/client";
import "@radix-ui/themes/styles.css";
import App from "./App.tsx";
import { Theme } from "@radix-ui/themes";

createRoot(document.getElementById("root")!).render(
  <Theme>
    <App />
  </Theme>
);

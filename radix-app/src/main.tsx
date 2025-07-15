import { createRoot } from "react-dom/client";
import "@radix-ui/themes/styles.css";
import App from "./App.tsx";
import { Theme, ThemePanel } from "@radix-ui/themes";

createRoot(document.getElementById("root")!).render(
  <Theme
    appearance="dark"
    accentColor="mint"
    grayColor="sand"
    radius="large"
    scaling="95%"
  >
    <App />
    <ThemePanel />
  </Theme>
);

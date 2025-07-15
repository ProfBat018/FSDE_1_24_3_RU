import { WeatherData } from "./WeatherData";
import "./App.css";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";

const queryClient = new QueryClient();

function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <div className="container mx-auto p-4 flex items-center justify-center">
        <WeatherData />
      </div>
    </QueryClientProvider>
  );
}

export default App;

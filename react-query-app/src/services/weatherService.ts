import axios from "axios";
import { type QueryFunctionContext } from "@tanstack/react-query";

export const fetchWeather = async ({
  queryKey,
}: QueryFunctionContext<[string, string]>) => {
  const [, cityName] = queryKey;

  const apiKey = import.meta.env.VITE_OPENWEATHER_API_KEY;

  const url = `https://api.openweathermap.org/data/2.5/weather?q=${cityName}&appid=${apiKey}`;

  const response = await axios.get(url);

  console.log(response.data);

  return response.data;
};

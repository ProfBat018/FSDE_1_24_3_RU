import { MagnifyingGlassIcon } from "@radix-ui/react-icons";
import { Button, Flex, TextField, Text, Box } from "@radix-ui/themes";
import { Icon } from "@radix-ui/themes/components/callout";
import { useQuery } from "@tanstack/react-query";
import { useRef, useState } from "react";
import { fetchWeather } from "./services/weatherService";

// export const WeatherData: React.FC = () => {
//   return (
//     <div className="flex flex-row items-center">
//       <TextField.Root
//         size="3"
//         className="w-lg"
//         placeholder="Search the weather in your city"
//       >
//         <TextField.Slot>
//           <MagnifyingGlassIcon height="16" width="16" />
//         </TextField.Slot>
//       </TextField.Root>
//       <Button variant="solid" size="3">
//         Search
//       </Button>
//     </div>
//   );
// };

export const WeatherData: React.FC = () => {
  const [cityName, setCityName] = useState<string>();

  const cityInput = useRef<HTMLInputElement>(null);

  const { data, isLoading, error } = useQuery({
    queryKey: ["cityName", cityName],
    queryFn: fetchWeather,
    enabled: !!cityName,
  });

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();

    setCityName(cityInput.current?.value);
  };

  return (
    <>
      <Flex direction="row" align="center" gap="3" width="100%">
        <form
          className="flex w-full items-center"
          onSubmit={(e) => handleSubmit(e)}
        >
          <TextField.Root
            ref={cityInput}
            placeholder="Search the weather in your city"
            size="3"
            className="flex-1"
          >
            <TextField.Slot>
              <Icon>
                <MagnifyingGlassIcon height="20" width="20" />
              </Icon>
            </TextField.Slot>
          </TextField.Root>

          <Button size="3" variant="solid">
            Search
          </Button>
        </form>
      </Flex>

      {isLoading && <Text mt="4">Загрузка...</Text>}
      {error && (
        <Text mt="4" color="red">
          Ошибка загрузки данных
        </Text>
      )}

      {data && (
        <Box mt="4" className="rounded border p-4 w-full max-w-md">
          <Text as="div" size="5" weight="bold">
            {data.name}, {data.sys.country}
          </Text>
          <Text>Температура: {data.main.temp} °C</Text>
          <Text>Ощущается как: {data.main.feels_like} °C</Text>
          <Text>Погода: {data.weather[0]?.description}</Text>
          <Text>Ветер: {data.wind.speed} м/с</Text>
          <Text>Влажность: {data.main.humidity} %</Text>
        </Box>
      )}
    </>
  );
};

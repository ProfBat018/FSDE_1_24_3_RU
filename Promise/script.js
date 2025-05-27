console.log("Начало выполнения скрипта");

const promise = new Promise((resolve, reject) => {
  setTimeout(() => {
    const success = true;
    if (success) {
      resolve("Успех!");
    } else {
      reject("Ошибка!");
    }
    console.log(
      "Это сообщение будет выведено после задержки, но до завершения промиса."
    );
  }, 1000);
});

promise
  .then((result) => console.log(result)) // Успех!
  .catch((error) => console.error(error)); // Ошибка!

console.log(
  "Это сообщение будет выведено сразу, не дожидаясь выполнения промиса."
);

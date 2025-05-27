# Тема урока: 
- Исключения и обработка ошибок
- Как работают события в JS 
- Для бонуса tailwindcss и как его подключить к проекту.
- Применение form 
- Promises и async/await 
- Пример приложения на JS и ASP.NET Core WEB API 


## Исключения и обработка ошибок 
- Исключения - это ошибки, которые возникают во время выполнения программы. Они могут быть вызваны различными причинами, такими как неправильный ввод данных, отсутствие файлов и т.д.

Синтаксис обработки исключений в JavaScript:
```javascript 

try {
    // Код, который может вызвать исключение
} catch (error) {
    // Код, который выполняется при возникновении исключения
} finally {
    // Код, который выполняется в любом случае
}
```

Пробрасывание ошибок:
```javascript
throw new Error('Сообщение об ошибке');
```

При это пробрасывать обычные литералы нельзя, только объекты. 
```javascript 

throw 'Сообщение об ошибке'; // Ошибка
throw 404; // Ошибка
```

- Пробрасывать можно только объекты. 
```javascript 
throw new Error('Сообщение об ошибке'); // Правильно
```

Или ваши кастомные ошибки. 
```javascript 

class MyError extends Error {
    constructor(messageб, code) {
        super(message);
        this.code = code;
        this.name = 'MyError';
    }

``

    toString() {
        return `${this.name}: ${this.message}: ${this.code}`;
    }
}
throw new MyError('Сообщение об ошибке'); // Правильно
```

## Как работают события в JS 

- События - это действия, которые происходят в браузере. Например, клик мыши, нажатие клавиши и т.д. 
- События могут быть вызваны пользователем или браузером. 

Под капотом события - это просто функции, которые вызываются при определенных условиях. Если вы хотите написать кастомное событие, то вам нужно создать объект события и вызвать его. 
```javascript 

const event = new Event('myEvent'); 

document.dispatchEvent(event); // Вызов события
``` 

В данном случае **dispath** очень похож на метод **invoke** в C#. 

Давайте рассмотрим эту тему на примере проекта который будет в папке `EventsFirst`. В нем мы напишем свою форму для логина и регистрации с использованием асинхронного кода, ASP.NET Web API и tailwindcss. Соответсвенно, для того чтобы я вам показал все это нам нужно для начала пройти несколько важных подтем. 

## Tailwindcss и как его подключить к проекту. 

`Tailwindcss` - это CSS-фреймворк, который позволяет быстро создавать адаптивные интерфейсы. Он основан на утилитарном подходе к стилям, что позволяет легко настраивать и комбинировать классы для создания уникальных дизайнов.

То есть у вас есть набор классов, которые вы можете комбинировать для создания нужного вам дизайна. Например, вы можете использовать классы `bg-red-500` и `text-white`, чтобы создать красную кнопку с белым текстом. На заднем плане это паросто css и js код. 

### Установка tailwindcss 

Для его установки нам понадобится `npm` и `node.js`. 
```bash 
npm install tailwindcss @tailwindcss/cli
```

Так как нам будет не удобно каждый раз запускать `npm` для компиляции css, мы можем использовать `npx` для этого. 
```bash 
npx tailwindcss -i ./src/input.css -o ./dist/output.css --watch
```

Но тут есть один нюанс, так как у нас будет несколько страниц, то нам нужно будет создать несколько файлов css. Поэтому мы создадим файл `tailwind.config.js` и добавим туда пути к нашим файлам. 
```javascript 
/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    './src/**/*.{html,js}',
    './dist/**/*.{html,js}',
  ],
  theme: {
    extend: {},
  },
  plugins: [],
}
```

После этого нам нужно будет скачать плагин `autoprefixer` и добавить его в конфиг. 
```bash 
npm install autoprefixer
```

```javascript
/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    './src/**/*.{html,js}',
    './dist/**/*.{html,js}',
  ],
  theme: {
    extend: {},
  },
  plugins: [
    require('autoprefixer')
  ],
}
```


После этого мы можем использовать `npx` для компиляции css. 
```bash
npx tailwindcss -i ./src/input.css -o ./dist/output.css --watch
```

## XMLHttpRequest и Fetch API 

Есть два основных способа работы с асинхронными запросами в JS: `XMLHttpRequest` и `Fetch API`. Если приводить аналогию с C#, то `XMLHttpRequest` - это как `WebClient`, а `Fetch API` - это как `HttpClient`.

`XMLHttpRequest` - это устаревший способ работы с асинхронными запросами, который используется в основном для совместимости с более старыми браузерами. Он позволяет отправлять HTTP-запросы и получать ответы от сервера. В современных приложениях такой подход уже не используется, но вот пример 

```javascript
const xhr = new XMLHttpRequest();

xhr.open('GET', 'https://jsonplaceholder.typicode.com/posts/1');
xhr.onload = function() {
    if (xhr.status === 200) {
        console.log(xhr.responseText);
    } else {
        console.error('Ошибка:', xhr.statusText);
    }
};
xhr.onerror = function() {
    console.error('Ошибка сети');
};
xhr.send();
```

`Fetch API` - это современный способ работы с асинхронными запросами, который основан на промисах. Он позволяет отправлять HTTP-запросы и получать ответы от сервера. 

```javascript

fetch('https://jsonplaceholder.typicode.com/posts/1')
    .then(response => {
        if (!response.ok) {
            throw new Error('Сеть ответила с ошибкой: ' + response.status);
        }
        return response.json();
    })
    .then(data => console.log(data))
    .catch(error => console.error('Ошибка:', error));
```

Как вы видите fetch работает с промисами, а значит мы можем использовать async/await. Давайте сначала разберем как работают промисы. 


## Promises и async/await 

- Промисы - это объекты, которые представляют собой результат асинхронной операции. Они могут находиться в одном из трех состояний: ожидание (pending), выполнено (fulfilled) или отклонено (rejected).

Если сделать аналогию с C#, то промис - это как `Task`, который может завершиться успешно или с ошибкой. 
```javascript 

const promise = new Promise((resolve, reject) => {
    setTimeout(() => {
        const success = true; 
        if (success) {
            resolve('Успех!');
        } else {
            reject('Ошибка!');
        }
    }, 1000);
});


promise
    .then(result => console.log(result)) // Успех!
    .catch(error => console.error(error)); // Ошибка!
```










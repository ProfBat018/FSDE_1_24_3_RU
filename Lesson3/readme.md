# Тема урока: Компоненты в React 
- Что такое компонент ? 
- Типы компонентов 
- Классовый компонент 
- state
- Шаги рендеринга компонента 

# Что такое компонент ? 

`Компонент` - это блок кода, который мы можем подсавить куда угодно. Данный код может состоять как из других компонентов, так и включать в себя `.css` файлы. Философия такого подхода в `ui` позволяет нам писать гибкий код. 


# Типы компонентов 

У нас есть два типа компонентов. 

1. Классовые компоненты 
2. Функциональные компоненты 

Для начала вам нужно понять что сперва были созданы классовые компоненты, которые в будущем эволюционировали в функциональные. Я более чем уверен что вы не будете использовать классовые, но эти  фундаментальные знания нужны для того чтобы мы с вами могли понять как рендериться компонент. 

# Классовый компонент 

```jsx
class Greeting extends Component {
  render() {
    return <h1>Hello, {this.props.name}!</h1>;
  }
}
```

как вы можете увидеть, каждый компонент наследуется от базового класса Component и в нем должен быть метод **render** . 

Одной из самых главных частей разработки **SPA** это управление состоянием компонентов, то есть `state managment`. Предположим нам нужно написать просто счетчик нажатий, как нам тогда быть ? 

Вот код работы state: 

```js
class MyComponent {
  constructor(props) {
    this.props = props;
    this.state = { count: 0 };

    // Привязываем setState к this
    this.setState = this.setState.bind(this);

    // Рендерим первый раз
    this.render();
  }

  setState(partialState) {
    // Если partialState — функция, вызываем её
    if (typeof partialState === 'function') {
      partialState = partialState(this.state, this.props);
    }
    // Обновляем state
    this.state = {
      ...this.state,
      ...partialState
    };
    // Перерисовываем компонент
    this.render();
  }

  render() {
    // "Рендерим" в консоль (или в DOM, если хочешь)
    console.log(`Count: ${this.state.count}`);
    // В реальном React тут бы был JSX и взаимодействие с DOM
  }

  // Пример метода для увеличения счётчика
  increment() {
    this.setState((prevState) => ({ count: prevState.count + 1 }));
  }
}

// Пример использования
const comp = new MyComponent();
comp.increment(); // Count: 1
comp.increment(); // Count: 2
```


Вот как надо определять state у компонента: 

```js

import React, { Component } from 'react';

class Counter extends Component {
    constructor(props) {
        super(props);
        this.state = {
            test: 'Counter Component',
            count: 0
        };
    }

    handleClick = () => {
        console.log(this.state);
        
        this.setState(prevState => ({
            count: prevState.count + 1
        }));
    };

    render() {
        return (
            <div>
                <button onClick={this.handleClick}>Click Me</button>
                <p>Count: {this.state.count}</p>
            </div>
        );
    }
}




export default Counter;
```

# Шаги рендеринга объекта 

В `React` есть встроенные функции которые отвечают за состояние рендеринга объекта. Например 
- ComponentDidMount 
- ComponentDidUpdate
- ComponrnyDidCatch
- ComponentWillMount
- ComponentWillUnMount 
- ComponentWillReceiveProps 








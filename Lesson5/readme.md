# Тема урока 
- High Order Components 
- useReducer 
- useCallbacks
- useMemo 


# [HOC (High Order Component)](https://habr.com/en/companies/ruvds/articles/428572/)

Компонент высшего порядка - это ваш адаптер на пути к гибкости вашего проекта. 
Можно ли обойтись без них ? да, но вы сами поймете когда они вам нужны. 


Предположим, у меня в проекте во многих еомпонентах есть проверка на аутентификацию пользователя. Данную проблему можно решить двумя способами. 
1. использовать `useContext`
2. использовать `HOC` 

`HOC` - это компонент обертка, в который мы оборачиваем наш компонент. Таким образом он добавляет определенный функционал нашим элементам. 

Давайте разберем на примере композиции или наследования в C#. 
Если я унаследую свой класс от другого, то я приму в себя его поведение, точно так же я смогу 
использовать логику другого класса, если он будет полем внутри моего. 


```js
function withHover(ParamComponent) {
  return class WithHover extends React.Component {
    state = { hovering: false }
    mouseOver = () => this.setState({ hovering: true })
    mouseOut = () => this.setState({ hovering: false })
    render() {
      return (
        <div onMouseOver={this.mouseOver} onMouseOut={this.mouseOut}>
          <ParamComponent hovering={this.state.hovering} />
        </div>
      );
    }
  }
}
```

# useReducer 

данный хук, точно так же как и useState предназначен для управления состоянием компонента. Его главное отличие в том, что он используется тогда, когда мы знаем какие состояния будут у компонента к примеру что-то вроде enum. Вот пример использования useReducer: 

```jsx 
import { useReducer } from 'react';

function reducer(state, action) {
  switch (action.type) {
    case 'incremented_age': {
      return {
        name: state.name,
        age: state.age + 1
      };
    }
    case 'changed_name': {
      return {
        name: action.nextName,
        age: state.age
      };
    }
  }
  throw Error('Unknown action: ' + action.type);
}

const initialState = { name: 'Taylor', age: 42 };

export default function Form() {
  const [state, dispatch] = useReducer(reducer, initialState);

  function handleButtonClick() {
    dispatch({ type: 'incremented_age' });
  }

  function handleInputChange(e) {
    dispatch({
      type: 'changed_name',
      nextName: e.target.value
    }); 
  }

  return (
    <>
      <input
        value={state.name}
        onChange={handleInputChange}
      />
      <button onClick={handleButtonClick}>
        Increment age
      </button>
      <p>Hello, {state.name}. You are {state.age}.</p>
    </>
  );
}

```

## useCallback 

`useCallback` - это хук, который позволяет вам избавиться от пересоздания функции во время перерендеров с использованием функций обратного вызова.   

## useMemo 

Данный хук предназначен для мемоизации данных, а не функции как с useCallback. Прекраснго использовать с пагинацией и фильтрацией данных. Пример в проекте movie-app 




# Тема урока: 
- keyof operator 
- Type Guards/Narrowing
- - instanceof
- - in operator
- - typeof operator
- - Type Predicates
- Extending Interfaces
- Hybrid Types


# Keyof operator

В TypeScript оператор `keyof` позволяет получить тип, представляющий ключи объекта. Это полезно для создания обобщенных функций и типов, которые могут работать с различными объектами.

```typescript
interface User {
  id: number;
  name: string;
  email: string;
}
type UserKeys = keyof User; // "id" | "name" | "email"

```

# Type Guards/Narrowing

Type Guards (или Narrowing) в TypeScript позволяют уточнять типы переменных во время выполнения программы. Это помогает 
избежать ошибок и сделать код более безопасным.

## instanceof
```typescript

class Animal {
  name: string;
  constructor(name: string) {
    this.name = name;
  }
}

class Dog extends Animal {
  bark() {
    console.log(`${this.name} says woof!`);
  }
}

```

## in operator

Данный оператор позволяет проверить, содержит ли объект определенное свойство. Это полезно для создания Type Guards.

```typescript
interface Cat {
    meow: string;
//   meow(): void;
}
interface Dog {
  bark(): void;
}

let myCat: Cat = {
    meow: "Meow!",
};
let myDog: Dog = {
    bark: () => console.log("Woof!"),
};


function isCat(animal: Cat | Dog): animal is Cat {
  return 'meow' in animal;
}
function makeSound(animal: Cat | Dog) {
  if (isCat(animal)) {
    console.log(animal.meow); // Outputs: Meow!
  } else {
    animal.bark();
  }
}

makeSound(myCat); // Outputs: Meow!
```

## typeof operator

Оператор `typeof` позволяет проверить тип переменной во время выполнения. Это полезно для создания Type Guards, особенно для примитивных типов.

```typescript

let value: string | number = "Hello";

console.log(typeof value); // Output: string

```

## Type Predicates
Type Predicates позволяют создавать функции, которые уточняют типы переменных. Это делается с помощью специального синтаксиса `parameterName is Type`.

```typescript
// function funcName(paramName: paramType): returnType

function isString(value: unknown): value is string {
  return (typeof value) === 'string';
}

function example(x: unknown) {
  if (isString(x)) {
    // We can now call any 'string' method on 'x'.
    x.toUpperCase();
  } else {
    console.log(x);
  }
}

example("Hello, TypeScript!"); // Outputs: "HELLO, TYPESCRIPT!"
example(42); // Outputs: 42
```









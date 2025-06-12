# Тема урока
- Enum 
- Array 
- Tuple 
- unknown
- any 
- Assertions 
- type
- Non-null assertion operator
- Satisfies 


# Enum 

Как и в других языках программирования, в TypeScript есть перечисления (enums). Перечисления позволяют создавать набор именованных констант. Это может быть полезно для создания набора связанных значений, которые можно использовать в коде.

```ts
enum Direction {
    Up = "UP",
    Down = "DOWN",
    Left = "LEFT",
    Right = "RIGHT"
}
function move(direction: Direction) {
    switch (direction) {
        case Direction.Up:
            console.log("Moving up");
            break;
        case Direction.Down:
            console.log("Moving down");
            break;
        case Direction.Left:
            console.log("Moving left");
            break;
        case Direction.Right:
            console.log("Moving right");
            break;
    }
}

```
# Array
Массивы в TypeScript могут быть объявлены с помощью синтаксиса `type[]` или `Array<type>`. Они позволяют хранить коллекции значений одного типа.

```ts
let numbers: number[] = [1, 2, 3, 4, 5];
let strings: Array<string> = ["apple", "banana", "cherry"];
function printArray(arr: number[]) {
    arr.forEach(num => console.log(num));
}
printArray(numbers);
```

Если вы хотите что-то на подобии IEnumerable в C#, то вы можете принимать в функцию `Iterable` или `ArrayLike`:

```ts
function printIterable(arr: Iterable<number>) {
    for (const num of arr) {
        console.log(num);
    }
}
let iterableNumbers: Iterable<number> = [1, 2, 3, 4, 5];
printIterable(iterableNumbers);
```

# Tuple

Кортежи (tuples) в TypeScript позволяют создавать массивы с фиксированным количеством элементов, где каждый элемент может иметь свой тип. Это полезно, когда нужно хранить связанные значения разных типов.

```ts
let person: [string, number] = ["Alice", 30];
function printPersonInfo(person: [string, number]) {
    console.log(`Name: ${person[0]}, Age: ${person[1]}`);
}
printPersonInfo(person);
```

Для того чтобы объявить кортеж, вам нужно указать типы элементов в квадратных скобках. Кортежи могут быть полезны для работы с данными, которые имеют фиксированную структуру, например, координаты (x, y) или дата и время. Точно так же, это было и в C#, где было 8 перегруженных типов кортежей. 

# unknown и any
`unknown` и `any` - это два типа в TypeScript, которые позволяют работать с неопределенными значениями, но с разными уровнями строгости.

`any` - это тип, который позволяет вам присваивать любое значение без проверки типов. Это может быть полезно, когда вы не уверены в типе данных, но использование `any` снижает преимущества статической типизации TypeScript.

```ts
let value: any = "Hello";
value = 42; // Можно присвоить любое значение
function printValue(val: any) {
    console.log(val);
}
printValue(value);
```
`unknown` - это более строгий тип, который также позволяет работать с неопределенными значениями, но требует явной проверки типа перед использованием. Это помогает избежать ошибок, связанных с неправильным использованием типов.

```ts
let value: unknown = "Hello";
value = 42; // Можно присвоить любое значение
function printValue(val: string) {
    if (typeof val === "string") {
        console.log(val.toUpperCase()); // Нужно проверить тип перед использованием
    } else {
        console.log("Not a string");
    }
}
printValue(value);
```

# Assertions
TypeScript позволяет использовать утверждения типов (type assertions) для указания компилятору, что вы уверены в типе значения. Это может быть полезно, когда вы знаете больше о типе данных, чем TypeScript.

Соответственно вы можете использовать синтаксис `as` или угловые скобки для утверждения типа.

```ts 
let value: any = "Hello";

let strLength: number = (value as string).length; // Использование as
let strLength2: number = (<string>value).length; // Использование угловых скобок
function printLength(val: any) {
    let str = val as string; // Утверждение типа
    console.log(str.length);
}
printLength(value);
```

#### as any & as const 
`as any` и `as const` - это два различных утверждения типов в TypeScript, которые используются для разных целей.

`as any` используется для указания компилятору, что вы хотите игнорировать проверку типов и разрешить любое значение. Это может быть полезно, когда вы работаете с динамическими данными или сторонними библиотеками, но использование `any` снижает преимущества статической типизации.


```ts

// Допустим, библиотека возвращает неизвестный формат
const response = getExternalData(); // type: unknown

// Нам нужно обойти строгую типизацию
const user = response as any;

// Используем без ошибки
console.log(user.name);
```

`as const` используется для указания компилятору, что вы хотите сделать значение неизменяемым (immutable) и сохранить его точный тип. Это полезно, когда вы хотите создать константу с фиксированными значениями, например, для массивов или объектов.

```ts

const roles = ['admin', 'user', 'guest'] as const;

type Role = typeof roles[number]; // "admin" | "user" | "guest"

function hasAccess(role: Role) {
  return role === 'admin';
}

hasAccess('user'); // ✅
hasAccess('moderator'); // ❌ TS Error
```

# type
В TypeScript `type` используется для определения пользовательских типов. Это позволяет создавать более сложные структуры данных, которые могут включать в себя примитивные типы, массивы, кортежи и другие пользовательские типы.

Не путайте с `interface`, который также используется для определения типов, но имеет некоторые отличия в синтаксисе и возможностях.

```ts
type User = {
    name: string;
    age: number;
    isAdmin?: boolean; // Необязательное свойство
};

function printUser(user: User) {
    console.log(`Name: ${user.name}, Age: ${user.age}, Admin: ${user.isAdmin ?? false}`);
}
let user: User = {
    name: "Alice",
    age: 30,
    isAdmin: true
};
printUser(user);
```

разница между `type` и `interface` заключается в том, что `type` может использоваться для создания объединений (union types) и пересечений (intersection types), а `interface` - для определения структуры объектов и классов. 

# Non-null assertion operator
Non-null assertion operator (`!`) в TypeScript используется для указания компилятору, что значение не является `null` или `undefined`. Это может быть полезно, когда вы уверены, что значение будет определено, но TypeScript не может это гарантировать.

```ts
let value: string | null = "Hello";
let length: number = value!.length; // Использование оператора ! для утверждения, что value не null
function printLength(val: string | null) {
    console.log(val!.length); // Использование оператора ! для утверждения, что val не null
}
printLength(value);
```

# Satisfies
`satisfies` - это новый оператор в TypeScript, который позволяет проверять, соответствует ли значение определенному типу. Он используется для проверки типов во время компиляции и может помочь избежать ошибок, связанных с неправильным использованием типов.

```ts

type User = {
    name: string;
    age: number;
};
let user = {
    name: "Alice",
    age: 30,
    isAdmin: true
} satisfies User; // Проверка, что user соответствует типу User

function printUser(user: User) {
    console.log(`Name: ${user.name}, Age: ${user.age}`);
}
printUser(user); // Ошибка компиляции, если user не соответствует User
```

    
# Тема урока: Объектные типы данных

На прошлом уроке мы с вами прошли `Interface` в `TS` и вы узнали что он нужен для того чтобы навять объекту какие либо свойтсва. Кроме интефейсов в `TS` есть еще и другие объектные типы данных, которые мы с вами сегодня рассмотрим.

# Объектные типы данных
- Class
- Enum
- Array 
- Tuple
- Object 

## Class

ООП часть в `TS` очень красивая. Мы можем заметить полную и человеческую синтаксическую структуру, которая позволяет нам создавать классы, наследовать их и использовать в них интерфейсы. Вот пример работы с классами в `TS`:

```typescript
interface User {
    name: string;
    age: number;
    greet(): void;
}

class UserClass implements User {
    name: string;
    age: number;

    constructor(name: string, age: number) {
        this.name = name;
        this.age = age;
    }

    greet() {
        console.log(`Hello, my name is ${this.name} and I am ${this.age} years old.`);
    }
}

const user = new UserClass("Alice", 30);
user.greet(); // Hello, my name is Alice and I am 30 years old.
```

Пример наследования классов в `TS`:

```typescript

class Transport {
    wheels: number;

    constructor(wheels: number) {
        this.wheels = wheels;
    }

    move() {
        console.log(`Moving with ${this.wheels} wheels.`);
    }
}

class Car extends Transport {
    brand: string;

    constructor(wheels: number, brand: string) {
        super(wheels);
        this.brand = brand;
    }

    drive() {
        console.log(`Driving a ${this.brand} car with ${this.wheels} wheels.`);
    }
}

const myCar = new Car(4, "Toyota");

myCar.move(); // Moving with 4 wheels.
myCar.drive(); // Driving a Toyota car with 4 wheels.
```

Переопределние методов в `TS` также возможно, вот пример:

```typescript 
class Animal {
    speak(): void {
        console.log("Animal speaks");
    }
}
class Dog extends
Animal {
    speak(): void {
        console.log("Dog barks");
    }
}

class Cat extends
Animal {
    speak(): void {
        console.log("Cat meows");
    }
}

let a: Animal = new Dog();
a.speak(); 

a = new Cat();
a.speak(); // Cat meows
```

Если вы хотите написать `getter` и `setter` в `TS`, то вот пример:

```typescript
class Person {
    private _name: string;
    
    constructor(name: string) {
        this._name = name;
    }

    get name(): string {
        console.log("Getting name");
        return this._name;
    }

    set name(newName: string) {
        console.log("Setting name");
        this._name = newName;
    }
}

const person = new Person("Alice");
console.log(person.name); // Alice
person.name = "Bob";
console.log(person.name); // Bob
```

Перегрузка методов в `TS` также поддерживается, вот пример:

```typescript
class Calculator {
    add(a: number, b: number): number;
    add(a: string, b: string): string;
    add(a: any, b: any): any {
        return a + b;
    }
}
const calc = new Calculator();
console.log(calc.add(5, 10)); // 15
``` 

Это всего лишь подобие перегрузки, так как в `TS` нет настоящей перегрузки методов, а есть только сигнатуры методов. 


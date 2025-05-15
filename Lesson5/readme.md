# Тема урока: 
- OOP 
- - синтаксис классов 
- - конструкторы
- - методы
- - свойства
- - геттеры и сеттеры
- - понятие prototype
- - наследование
- - полиморфизм

## OOP в JavaScript

Каких-то сильных изменений в ООП в JS не произошло. В принципе все так же как и было. В `React` классовые компоненты уже не используются, но в `Vue` они все еще актуальны. Если вы будете писать Back-End на `Node.js`, то там точно понадобится писать классы. 

## Синтаксис классов

```javascript
class User {
    constructor(name, age) {
        this.name = name;
        this.age = age;
    }

    sayHello() {
        console.log(`Hello, my name is ${this.name}`);
    }
}

const user = new User('John', 25);

user.sayHello(); // Hello, my name is John
```
## Конструкторы
Конструктор - это специальный метод, который вызывается при создании нового экземпляра класса. Он используется для инициализации свойств объекта.

```javascript
class User {
    constructor(name, age) {
        this.name = name;
        this.age = age;
    }
}
```

Перегрузка конструктора в JS не поддерживается. Но можно использовать `default` значения для параметров. 

```javascript
class User {
    constructor(name = 'John', age = 25) {
        this.name = name;
        this.age = age;
    }
}
```

## Методы
Методы - это функции, которые определены внутри класса. Они могут использоваться для выполнения действий с объектом или для получения информации о нем.

```javascript
class User {
    constructor(name, age) {
        this.name = name;
        this.age = age;
    }

    sayHello() {
        console.log(`Hello, my name is ${this.name}`);
    }
}

```

this не работает так, как работает в объектах 
```javascript
class User {
    constructor(name, age) {
        this.name = name;
        this.age = age;
    }

    sayHello() {
        console.log(`Hello, my name is ${this.name}`);
    }
    sayHelloArrow = () => {
        console.log(`Hello, my name is ${this.name}`);
    }
}
```

## Свойства
Свойства - это переменные, которые определены внутри класса. Они могут использоваться для хранения информации о объекте.

```javascript
class User {
    constructor(name, age) {
        this.name = name;
        this.age = age;
    }
}
```

Если вы хотите сделать свойство приватным, то нужно использовать `#` перед именем свойства. 

```javascript 
class User {
    #name;
    #age;

    constructor(name, age) {
        this.#name = name;
        this.#age = age;
    }

    sayHello() {
        console.log(`Hello, my name is ${this.#name}`);
    }
}
```
Конечно же вы можете использовать `get` и `set` для работы с приватными свойствами. 

```javascript
class User {
    #name;
    #age;

    constructor(name, age) {
        this.#name = name;
        this.#age = age;
    }

    get name() {
        return this.#name;
    }

    set name(name) {
        this.#name = name;
    }

    sayHello() {
        console.log(`Hello, my name is ${this.#name}`);
    }
}
```

## Наследование
Наследование - это механизм, который позволяет создавать новый класс на основе существующего класса. Новый класс наследует все свойства и методы родительского класса.

```javascript
class User {
    constructor(name, age) {
        this.name = name;
        this.age = age;
    }

    sayHello() {
        console.log(`Hello, my name is ${this.name}`);
    }
}

class Admin extends User {
    constructor(name, age, role) {
        super(name, age);
        this.role = role;
    }

    sayHello() {
        console.log(`Hello, my name is ${this.name} and I am an ${this.role}`);
    }
}

const user = new User("John", 30);
const admin = new Admin("Jane", 28, "admin");

user.sayHello(); // Hello, my name is John
admin.sayHello(); // Hello, my name is Jane and I am an admin
console.log(user instanceof User); // true
console.log(user instanceof Admin); // true

```

## Полиморфизм

Привычных для вам virtual методов в JS нет. Но вы можете переопределить метод родительского класса в дочернем классе. 

```javascript
class User {
    constructor(name, age) {
        this.name = name;
        this.age = age;
    }

    sayHello() {
        console.log(`Hello, my name is ${this.name}`);
    }
}

class Admin extends User {
    constructor(name, age, role) {
        super(name, age);
        this.role = role;
    }

    // sayHello() {
    //     console.log(`Hello, my name is ${this.name} and I am an ${this.role}`);
    // }
}
```

## Понятие prototype

В JS все объекты имеют свойство `prototype`. Это свойство используется для реализации наследования. Когда вы создаете новый объект, он наследует все свойства и методы родительского объекта. 

```javascript
class User {
    constructor(name, age) {
        this.name = name;
        this.age = age;
    }

    sayHello() {
        console.log(`Hello, my name is ${this.name}`);
    }
}

class Admin extends User {
    constructor(name, age, role) {
        super(name, age);
        this.role = role;
    }

    sayHello() {
        console.log(`Hello, my name is ${this.name} and I am an ${this.role}`);
    }
}

const user = new User("John", 30);
const admin = new Admin("Jane", 28, "admin");
console.log(user.__proto__); // User {}
console.log(admin.__proto__); // Admin {}
console.log(user.__proto__ === User.prototype); // true

```

## Полиморфизм

Полиморфизм - это возможность использовать один и тот же метод для разных объектов. В JS это реализуется через переопределение методов в дочерних классах как в примере выше. 

При этом нет никакиз интерфесов, по крайней мере в JS. Но вы можете использовать `abstract` классы и `interface` в TypeScript. 




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

class Car 
{

}

const user = new User("John", 30);
const admin = new Admin("Jane", 28, "admin");
console.log(user.__proto__); // {}
console.log(admin.__proto__); // User {}
console.log(user.__proto__ === User.prototype); // true

console.log(user);

var dataToSend = localStorage.getItem("dataToSend");

await fetch("https://jsonplaceholder.typicode.com/posts/1", {
    method: "POST",
    headers: {
        "Content-Type": "application/json",
    },
    body: JSON.stringify(dataToSend),
})


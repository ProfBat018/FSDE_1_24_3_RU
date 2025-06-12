


interface UserInterface {
    name: string;
    age: number;
    email: string;
    greet(): void;
}

const user: UserInterface = {
    name: "John Doe",
    age: 30,
    email: "",
    greet() {
        console.log(`Hello, my name is ${this.name}.`);
    }
};

    
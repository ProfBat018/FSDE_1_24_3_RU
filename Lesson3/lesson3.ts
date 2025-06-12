/*
enum Directions {
    Up,
    Down,
    Left,
    Right
}

function move(direction: Directions): string {
    switch (direction) {
        case Directions.Up:
            return "Moving Up";
        case Directions.Down:
            return "Moving Down";
        case Directions.Left:
            return "Moving Left";
        case Directions.Right:
            return "Moving Right";
        default:
            return "Unknown Direction";
    }
}

console.log(Directions.Up);

console.log(move(Directions.Up)); // Moving Up

*/

/*

function printIterable(arr: Iterable<number>) {
    for (const num of arr) {
        console.log(num);
    }
}
let iterableNumbers: Iterable<number> = [1, 2, 3, 4, 5];
let iterableNums2: Array<number> = [6, 7, 8, 9, 10];

printIterable(iterableNumbers);
printIterable(iterableNums2);

*/

type User = {
    name: string;
    age: number;
};

let user = {
    name: "Alice",
    age: 30,
    isAdmin: true
} satisfies User; 

function printUser(user: User) {
    console.log(`Name: ${user.name}, Age: ${user.age}`);
}
printUser(user); 


// let isActive: boolean = true;
// let count: number = 1;
// isActive = count; // Ошибка: Type 'number' is not assignable to type 'boolean'.
// count = isActive; // Ошибка: Type 'boolean' is not assignable to type 'number'.


let isActive: boolean = true;
let count: number = 1;
isActive = Boolean(count); // Приведение типа: число к булевому
isActive = count as unknown as boolean; // Приведение типа с использованием 'as'
count = Number(isActive); // Приведение типа: булевый к числу
count = isActive ? 1 : 0; // Приведение типа с использованием тернарного оператора


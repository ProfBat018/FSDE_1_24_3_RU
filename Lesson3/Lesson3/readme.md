# Тема урока: 
- Classes 
- Structs 
- Inheritance 
- Constructors 

Название класса нужно ставить по Pascal Case 

## Модификаторы доступа 

![](https://i.stack.imgur.com/TNtq3.png)

## Разница между классами и структурами в C# 

Классы: Передаются по ссылке. Если вы измените объект через одну переменную, изменения будут видны через другие переменные, указывающие на тот же объект.

Структуры: Передаются по значению. Каждая переменная получает свою копию, и изменения в одной переменной не влияют на другие.


Классы: Можно наследовать (support inheritance) и использовать полиморфизм.

Структуры: Не поддерживают наследование, кроме неявного наследования от System.ValueType.

Классы:
Могут иметь конструкторы по умолчанию (без параметров), если вы не создадите свой.
Поддерживают деструкторы.

Структуры:
Должны иметь конструктор с параметрами если вы инициализируете поле
Не поддерживают деструкторы.


Классы: Могут быть равны null.

Структуры: Не могут быть null, так как являются значимыми типами. Однако их можно сделать nullable, используя Nullable<T> или ? (например, int?).


Классы: Используются для сложных объектов, которые требуют ссылочной семантики или больших объемов данных.

Структуры: Предназначены для небольших, простых объектов, таких как точки, цветовые значения, координаты и т.д.
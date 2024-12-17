Cat a1 = new Cat();
Animal a2 = new Dog();
a1.Eat();

a1.name = "Barsik"; 

abstract class Animal 
{
    public abstract void Voice();

    public string name = "Animal";
    public void Eat()
    {
        Console.WriteLine("Animal is eating");
    }
}

class Dog : Animal
{
    public override void Voice()
    {
        Console.WriteLine("Gav");
    }
}

class Cat : Animal
{
    public override void Voice()
    {
        Console.WriteLine("Meow");
    }
}


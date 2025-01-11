
ShapeService shapeService = new ShapeService(new Circle());
shapeService.DrawShape();


abstract class Shape
{
    public abstract void Draw();
}

class Circle : Shape
{
    public override void Draw()
    {
        Console.WriteLine("Drawing a circle");
    }
}

class Rectangle : Shape
{
    public override void Draw()
    {
        Console.WriteLine("Drawing a rectangle");
    }
}

class ShapeService
{
    private readonly Shape _shape;

    public ShapeService(Shape shape)
    {
        _shape = shape;
    }
    
    public void DrawShape()
    {
        _shape.Draw();
    }
}
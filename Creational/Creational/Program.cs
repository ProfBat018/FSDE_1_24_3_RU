#region Singleton
/*
var singleton = SingletonObject.GetInstance();
var singleton2 = SingletonObject.GetInstance();
Console.WriteLine(singleton == singleton2); // True, both references point to the same instance

class SingletonObject
{
    private static SingletonObject _instance;
    
    private SingletonObject() {}
    
    public static SingletonObject GetInstance()
    {
        if (_instance == null)
        {
            _instance = new SingletonObject();
        }
        return _instance;
    }
}

*/


#endregion

#region Builder

var builder = new ComplexObjectBuilder();

builder.SetProperty1("Test");

var complexObject = builder.Build();

class ComplexObject
{
    public string Property1 { get; set; }
    public int Property2 { get; set; }
    public bool Property3 { get; set; }
}

class ComplexObjectBuilder
{
    private ComplexObject _complexObject;
    
    public ComplexObjectBuilder()
    {
        _complexObject = new ComplexObject();
    }
    
    public ComplexObjectBuilder SetProperty1(string value)
    {
        _complexObject.Property1 = value;
        return this;
    }
    
    public ComplexObjectBuilder SetProperty2(int value)
    {
        _complexObject.Property2 = value;
        return this;
    }
    
    public ComplexObjectBuilder SetProperty3(bool value)
    {
        _complexObject.Property3 = value;
        return this;
    }
    
    public ComplexObject Build()
    {
        return _complexObject;
    }
}


#endregion





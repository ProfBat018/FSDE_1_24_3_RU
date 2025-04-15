namespace Bridge.Model;

public class Train : ITransport
{
    public string Make { get; set; }
    public string Model { get; set; }
    public IEntity TransportEntity { get; set; }

    override public string ToString()
    {
        return $"{Make} {Model} {TransportEntity.GetType().Name}";
    }
    
}
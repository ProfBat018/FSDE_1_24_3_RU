using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bridge.Model;

namespace Bridge.Services;

class TransportService
{
    public ITransport CreateTransport()
    {
        TransportType type = new();

        int selection;
        var transportTypes = type.GetType().GetEnumNames();

        Console.WriteLine("Enter transport type:");

        for (int i = 0; i < transportTypes.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {transportTypes[i]}");
        }

        bool numberCheck = Int32.TryParse(Console.ReadLine(), out selection);
        
        if (numberCheck)
        {
            var transportType = GetTransportType((TransportType)selection);
            return GetTransport(transportType);
        }
        throw new ArgumentException("Selection is wrong");
    }

    public Type GetTransportType(TransportType userSelection) => userSelection switch
    {
        TransportType.Cargo => typeof(Cargo),
        TransportType.Passenger => typeof(Passenger),
        _ => throw new ArgumentException("Selection is wrong"),
    };

    public ITransport GetTransport(Type transportType)
    {
        var assembly = Assembly.GetExecutingAssembly(); // Рефлексия 
        
        List<Type> types = assembly.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(ITransport))).ToList();

        for (int i = 1; i < types.Count + 1; i++)
        {
            Console.WriteLine($"{i}. {types[i - 1].Name}");
        }
        
        Console.WriteLine("Select transport Name:");

        int selection = 0;
        
        bool numberCheck = Int32.TryParse(Console.ReadLine(), out selection);

        if (!numberCheck || selection < 1 || selection > types.Count)
        {
            throw new ArgumentException("Selection is wrong");
        }
        
        // var car = new Car();
        // ITransport transport = car;
        
        var transport = Activator.CreateInstance(types[selection - 1]) as ITransport;
        
        Console.WriteLine("Enter make:");
        transport.Make = Console.ReadLine();

        Console.WriteLine("Enter model:");
        transport.Model = Console.ReadLine();

        transport.TransportEntity = Activator.CreateInstance(transportType) as IEntity;

        return transport;
    }
}


#region Binary

/*
using System.Runtime.Serialization.Formatters.Binary;
#pragma warning disable SYSLIB0011


// SerializeToFile(new()
// {
//     Name = "Elvin",
//     Surname = "Azimov",
//     Age = 23
// });


Console.WriteLine(DeserializeFromFile());

void SerializeToFile(Person obj)
{
    var formatter = new BinaryFormatter();

    using var fs = new FileStream("users.bin", FileMode.OpenOrCreate);

    formatter.Serialize(fs, obj);
}

Person? DeserializeFromFile()
{
    var formatter = new BinaryFormatter();

    using var fs = new FileStream("users.bin", FileMode.OpenOrCreate);

    return formatter.Deserialize(fs) as Person;
}


[Serializable]
class Person
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }

    public override string ToString()
    {
        return $"{Name} {Surname} {Age}";
    }
}
*/

#endregion

#region XML
/*
using System.Data;
using System.Xml.Serialization;


// SerializeToFile(new List<Person>()
// {
//     new()
//     {
//         Name = "Elvin",
//         Surname = "Azimov",
//         Age = 23
//     }
// });

DeserializeFromFile().ForEach(x => Console.WriteLine(x));
void SerializeToFile(List<Person> obj)
{
    var formatter = new XmlSerializer(typeof(List<Person>));

    using var fs = new FileStream("users.xml", FileMode.OpenOrCreate);

    formatter.Serialize(fs, obj);
}


List<Person>? DeserializeFromFile()
{
    var formatter = new XmlSerializer(typeof(List<Person>));

    using var fs = new FileStream("users.xml", FileMode.OpenOrCreate);

    return formatter.Deserialize(fs) as List<Person>;
}
*/
#endregion

#region Json

/*
using System.Security.Cryptography;
using System.Text.Json;

var people = new List<Person>()
{
    new()
    {
        Name = "Elvin",
        Surname = "Azimov",
        Age = 23
    },
    new()
    {
        Name = "Maqa",
        Surname = "Babayev",
        Age = 18
    },
    new()
    {
        Name = "Ayxan",
        Surname = "Abbasov",
        Age = 20
    }
};


// SerializeToFile(people);

DeserializeFromFile()?.ForEach(x => Console.WriteLine(x));


void SerializeToFile(List<Person> obj)
{
    var json = JsonSerializer.Serialize(obj);

    File.WriteAllText("users.json", json);
}

List<Person>? DeserializeFromFile()
{
    var json = File.ReadAllText("users.json");

    return JsonSerializer.Deserialize<List<Person>>(json);
}
*/

#endregion

public class Person
{
    public Person()
    {
    }

    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }

    public override string ToString()
    {
        return $"{Name} {Surname} {Age}";
    }
}
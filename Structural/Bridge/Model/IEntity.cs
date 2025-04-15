using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Model;

public interface IEntity
{
    // данный метод с помощью рефлексии возвращает все свойства класса
    public PropertyInfo[] GetMetadata()
    {
        return this.GetType().GetProperties();
    }
}

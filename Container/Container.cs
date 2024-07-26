using System;
using System.Collections.Generic;

namespace DeveloperSample.Container
{
    public class Container
    {
        private readonly IDictionary<dynamic, dynamic> myType = new Dictionary<dynamic, dynamic>();
        public void Bind(dynamic interfaceType, dynamic implementationType)
        {
            myType.Add(interfaceType, implementationType);
        }
        public T Get<T>()
        {
            var t = typeof(T);
            if(myType.TryGetValue(t, out var type)) {
                return (T)Activator.CreateInstance(type);
            }
            throw new InvalidOperationException("No binding found for " + type);
        }
    }
}
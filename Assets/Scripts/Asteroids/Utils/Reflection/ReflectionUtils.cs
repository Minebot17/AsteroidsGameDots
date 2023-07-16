using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Asteroids.Utils.Reflection
{
    public static class ReflectionUtils
    {
        public static List<Type> GetTypesWithAttribute(this Type attributesType)
        {
            var allTypes = Assembly.GetExecutingAssembly().GetTypes();
            return allTypes
                .Where(t => t.GetCustomAttribute(attributesType) != null)
                .ToList();
        }
    }
}
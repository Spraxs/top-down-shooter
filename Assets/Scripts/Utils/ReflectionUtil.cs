using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public class ReflectionUtil
{

    public static T GetAttributeFromType<T>(Type type)
    {
        foreach (object obj in type.GetCustomAttributes())
        {
            if (typeof(T) == obj.GetType()) return (T)obj;
        }

        return default(T);
    }

    public static V GetTypeAttributeValue<T, V>(Type type, string fieldName)
    {
        T attribute = GetAttributeFromType<T>(type);

        FieldInfo field = attribute.GetType().GetField(fieldName);

        if (field == null) return default(V);

        object srcValue = attribute.GetType()
        .InvokeMember(field.Name,
        BindingFlags.GetField,
        null,
        attribute,
        null);

        return (V)srcValue;
    }

    public static MethodInfo[] GetMethodsWithAttribute<T>(Type commandType)
    {
        List<MethodInfo> methodList = new List<MethodInfo>();

        foreach (MethodInfo method in commandType.GetMethods())
        {
            foreach (object obj in method.GetCustomAttributes())
            {
                if (typeof(T) == obj.GetType())
                {
                    methodList.Add(method);
                    Console.WriteLine("Method gevonden met attribute: " + typeof(T).Name);
                }
            }
        }

        return methodList.ToArray<MethodInfo>();
    }

    public static T GetAttributeFromMethod<T>(MethodInfo method)
    {
        foreach (object obj in method.GetCustomAttributes())
        {
            if (typeof(T) == obj.GetType()) return (T)obj;
        }

        return default(T);
    }


    /**
     * 
     * T = Attribute Type
     * V = Value data type
     * 
     **/

    public static V GetMethodAttributeValue<T, V>(MethodInfo method, string fieldName)
    {
        List<MethodInfo> methodList = new List<MethodInfo>();

        T attribute = GetAttributeFromMethod<T>(method);

        if (attribute == null) return default(V);

        FieldInfo field = attribute.GetType().GetField(fieldName);

        if (field == null) return default(V);

        object srcValue = attribute.GetType()
        .InvokeMember(field.Name,
        BindingFlags.GetField,
        null,
        attribute,
        null);

        if (srcValue == null) return default(V); //TODO Make custom exceptions

        //if ()

        return (V)srcValue;
    }
}

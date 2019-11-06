using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

public class PacketManager
{
    private Assembly assembly;

    private Dictionary<int, Type> packetInClasses = new Dictionary<int, Type>();

    public static PacketManager Instance;

    public static void Init()
    {
        Instance = new PacketManager();
    }

    public PacketManager()
    {
        assembly = Assembly.GetExecutingAssembly();

        RegisterPacketInClasses();
    }

    private void RegisterPacketInClasses()
    {
        foreach (Type type in assembly.GetTypes())
        {

            if (type.IsSubclassOf(typeof(PacketIn)))
            {

                PacketIdAttribute packetIdAttribute = ReflectionUtil.GetAttributeFromType<PacketIdAttribute>(type);

                if (packetIdAttribute == null)
                {
                    Debug.LogError("PacketIn " + type.Name + " has no PacketIdAttribute set");
                    continue;
                }

                packetInClasses.Add(packetIdAttribute.Value, type);
            }
        }
    }

    public void HandlePacketIn(byte[] bytes)
    {
        MemoryStream memoryStream = new MemoryStream(bytes);

        int id = ReadShort(memoryStream);

        Type packetInClass = packetInClasses[id];

        PacketIn packetIn = (PacketIn) Activator.CreateInstance(packetInClass, memoryStream);

        FieldInfo[] fields = packetInClass.GetFields();

        foreach (FieldInfo fieldInfo in fields)
        {
            object value = packetIn.ReadNext(fieldInfo.FieldType);

            fieldInfo.SetValue(packetIn, value);
        }

        packetIn.OnDataHandled();

    }

    public int ReadShort(MemoryStream memoryStream)
    {
        byte[] byteArray = new byte[2];
        byteArray[0] = (byte)memoryStream.ReadByte();
        byteArray[1] = (byte)memoryStream.ReadByte();
        return BitConverter.ToInt16(byteArray, 0);
    }
}

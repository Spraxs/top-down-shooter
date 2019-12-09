using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

public class PacketManager
{
    private Assembly assembly;

    private Dictionary<int, PacketIn> packetInObjects = new Dictionary<int, PacketIn>();

    public static PacketManager Instance;

    private UnityMainThreadDispatcher unityMainThreadDispatcher;

    public static void Init()
    {
        Instance = new PacketManager(UnityMainThreadDispatcher.Instance());
    }

    public PacketManager(UnityMainThreadDispatcher unityMainThreadDispatcher)
    {
        this.unityMainThreadDispatcher = unityMainThreadDispatcher;
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


                PacketIn packetIn = (PacketIn) Activator.CreateInstance(type);

                packetInObjects.Add(packetIdAttribute.Value, packetIn);
            }
        }
    }

    public void HandlePacketIn(byte[] bytes)
    {
        MemoryStream memoryStream = new MemoryStream(bytes);

        int id = ReadShort(memoryStream);

        PacketIn packetIn = packetInObjects[id];

        if (packetIn.handleOnUnityThread)
        {
            unityMainThreadDispatcher.Enqueue(() => packetIn.HandleData(memoryStream));
        }
        else
        {
            packetIn.HandleData(memoryStream);
        }
    }

    public int ReadShort(MemoryStream memoryStream)
    {
        byte[] byteArray = new byte[2];
        byteArray[0] = (byte)memoryStream.ReadByte();
        byteArray[1] = (byte)memoryStream.ReadByte();
        return BitConverter.ToInt16(byteArray, 0);
    }
}

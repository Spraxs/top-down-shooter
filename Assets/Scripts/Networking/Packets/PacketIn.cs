using System;
using System.IO;
using System.Text;

public abstract class PacketIn
{
    public bool handleOnUnityThread;

    protected PacketIn(bool handleOnUnityThread)
    {
        this.handleOnUnityThread = handleOnUnityThread;
    }

    public abstract void HandleData(MemoryStream memoryStream);

    public object ReadNext(MemoryStream memoryStream, Type type)
    {

        if (type.Equals(typeof(string)))
        {
            return ReadString(memoryStream);
        }

        if (type.Equals(typeof(byte)))
        {
            return ReadByte(memoryStream);
        }

        if (type.Equals(typeof(short)))
        {
            return ReadShort(memoryStream);
        }

        if (type.Equals(typeof(int)))
        {
            return ReadInt(memoryStream);
        }

        if (type.Equals(typeof(long)))
        {
            return ReadLong(memoryStream);
        }

        if (type.Equals(typeof(float)))
        {
            return ReadFloat(memoryStream);
        }

        if (type.Equals(typeof(double)))
        {
            return ReadDouble(memoryStream);
        }

        return null;
    }

    public string ReadString(MemoryStream memoryStream)
    {
        return Encoding.UTF8.GetString(ReadBytes(memoryStream, memoryStream.ReadByte()));
    }

    public byte[] ReadBytes(MemoryStream memoryStream, int length)
    {
        byte[] result = new byte[length];
        for (int i = 0; i < length; i++)
        {
            result[i] = (byte)memoryStream.ReadByte();
        }
        return result;
    }

    public int ReadByte(MemoryStream memoryStream)
    {
        return memoryStream.ReadByte();
    }

    public int ReadShort(MemoryStream memoryStream)
    {
        byte[] byteArray = new byte[2];
        byteArray[0] = (byte)memoryStream.ReadByte();
        byteArray[1] = (byte)memoryStream.ReadByte();
        return BitConverter.ToInt16(byteArray, 0);
    }

    public int ReadInt(MemoryStream memoryStream)
    {
        byte[] byteArray = new byte[4];
        byteArray[0] = (byte)memoryStream.ReadByte();
        byteArray[1] = (byte)memoryStream.ReadByte();
        byteArray[2] = (byte)memoryStream.ReadByte();
        byteArray[3] = (byte)memoryStream.ReadByte();
        return BitConverter.ToInt32(byteArray, 0);
    }

    public long ReadLong(MemoryStream memoryStream)
    {
        byte[] byteArray = new byte[8];
        byteArray[0] = (byte)memoryStream.ReadByte();
        byteArray[1] = (byte)memoryStream.ReadByte();
        byteArray[2] = (byte)memoryStream.ReadByte();
        byteArray[3] = (byte)memoryStream.ReadByte();
        byteArray[4] = (byte)memoryStream.ReadByte();
        byteArray[5] = (byte)memoryStream.ReadByte();
        byteArray[6] = (byte)memoryStream.ReadByte();
        byteArray[7] = (byte)memoryStream.ReadByte();
        return BitConverter.ToInt64(byteArray, 0);
    }

    public float ReadFloat(MemoryStream memoryStream)
    {
        byte[] byteArray = new byte[4];
        byteArray[0] = (byte)memoryStream.ReadByte();
        byteArray[1] = (byte)memoryStream.ReadByte();
        byteArray[2] = (byte)memoryStream.ReadByte();
        byteArray[3] = (byte)memoryStream.ReadByte();
        return BitConverter.ToSingle(byteArray, 0);
    }

    public double ReadDouble(MemoryStream memoryStream)
    {
        byte[] byteArray = new byte[8];
        byteArray[0] = (byte)memoryStream.ReadByte();
        byteArray[1] = (byte)memoryStream.ReadByte();
        byteArray[2] = (byte)memoryStream.ReadByte();
        byteArray[3] = (byte)memoryStream.ReadByte();
        byteArray[4] = (byte)memoryStream.ReadByte();
        byteArray[5] = (byte)memoryStream.ReadByte();
        byteArray[6] = (byte)memoryStream.ReadByte();
        byteArray[7] = (byte)memoryStream.ReadByte();
        return BitConverter.ToDouble(byteArray, 0);
    }
}
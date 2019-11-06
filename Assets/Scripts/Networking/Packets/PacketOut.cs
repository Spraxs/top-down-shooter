
using System;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;

public abstract class PacketOut
{

    private MemoryStream memoryStream = new MemoryStream();

    public short id = -1;

    public abstract void onDataPrepare();

    public void HandlePacketData()
    {
        if (id < 0)
        {
            throw new SystemException("Packet out id has not been set!");
        }

        FieldInfo[] fields = GetType().GetFields();

        FieldInfo idField = fields[fields.Length - 1];

        fields = new FieldInfo[GetType().GetFields().Length];

        fields[0] = idField;

        for (int i = 1; i < fields.Length; i++)
        {
            fields[i] = GetType().GetFields()[i - 1];
        }

        foreach (FieldInfo field in fields)
        {
            object value = field.GetValue(this);

            WriteNext(field.FieldType, value);

            Debug.Log("Packet out field " + field.Name + " set to " + value.ToString());
        }
    }

    private void WriteNext(Type type, object value)
    {
        if (type.Equals(typeof(string)))
        {
            WriteString((string) value);
            return;
        }

        if (type.Equals(typeof(byte)))
        {
            WriteByte((byte) value);
            return;
        }

        if (type.Equals(typeof(short)))
        {
            WriteShort((short) value);
            return;
        }

        if (type.Equals(typeof(int)))
        {
            WriteInt((int) value);
            return;
        }

        if (type.Equals(typeof(long)))
        {
            WriteLong((long) value);
            return;
        }

        /*

        if (type.Equals(typeof(float)))
        {
            WriteFloat((float) value);
            return;
        }

        */

        if (type.Equals(typeof(double)))
        {
            WriteDouble((double) value);
            return;
        }

        throw new SystemException("Value type " + type.Name + " is not supported by packets!");
    }

    private void WriteString(String value)
    {
        if (value != null)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(value);
            WriteByte(byteArray.Length);
            WriteBytes(byteArray);
        }
        else
        {
            memoryStream.WriteByte(0);
        }
    }

    private void WriteBytes(byte[] byteArray)
    {
        for (int i = 0; i < byteArray.Length; i++)
        {
            memoryStream.WriteByte(byteArray[i]);
        }
    }

    private void WriteByte(int value)
    {
        memoryStream.WriteByte((byte)value);
    }

    private void WriteShort(int value)
    {
        memoryStream.WriteByte((byte)value);
        memoryStream.WriteByte((byte)(value >> 8));
    }

    private void WriteInt(int value)
    {
        memoryStream.WriteByte((byte)value);
        memoryStream.WriteByte((byte)(value >> 8));
        memoryStream.WriteByte((byte)(value >> 16));
        memoryStream.WriteByte((byte)(value >> 24));
    }

    private void WriteLong(long value)
    {
        memoryStream.WriteByte((byte)value);
        memoryStream.WriteByte((byte)(value >> 8));
        memoryStream.WriteByte((byte)(value >> 16));
        memoryStream.WriteByte((byte)(value >> 24));
        memoryStream.WriteByte((byte)(value >> 32));
        memoryStream.WriteByte((byte)(value >> 40));
        memoryStream.WriteByte((byte)(value >> 48));
        memoryStream.WriteByte((byte)(value >> 56));
    }

    // TODO: WriteFloat (SingleToInt32Bits)
    // private void WriteFloat(float fvalue)
    // {
    // long value = BitConverter.SingleToInt32Bits(fvalue);
    // memoryStream.WriteByte((byte)value);
    // memoryStream.WriteByte((byte)(value >> 8));
    // memoryStream.WriteByte((byte)(value >> 16));
    // memoryStream.WriteByte((byte)(value >> 24));
    // }

    private void WriteDouble(double dvalue)
    {
        long value = BitConverter.DoubleToInt64Bits(dvalue);
        memoryStream.WriteByte((byte)value);
        memoryStream.WriteByte((byte)(value >> 8));
        memoryStream.WriteByte((byte)(value >> 16));
        memoryStream.WriteByte((byte)(value >> 24));
        memoryStream.WriteByte((byte)(value >> 32));
        memoryStream.WriteByte((byte)(value >> 40));
        memoryStream.WriteByte((byte)(value >> 48));
        memoryStream.WriteByte((byte)(value >> 56));
    }

    public byte[] GetSendableBytes()
    {
        return memoryStream.ToArray();
    }
}
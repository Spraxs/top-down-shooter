using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
public class PacketIdAttribute : Attribute
{

    public PacketIdAttribute(int argsAmount)
    {

        Value = argsAmount;
    }

    public PacketIdAttribute()
    {

        Value = 0;
    }

    public int Value;
}
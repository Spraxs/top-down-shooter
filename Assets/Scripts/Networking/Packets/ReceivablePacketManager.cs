
using System;
using UnityEngine;

public class ReceivablePacketManager
{

    public static void Handle(PacketIn packet)
    {
        switch (packet.ReadShort())
        {
            case 0:
                Debug.Log("Player connected!");
                break;
            case 1:
                Debug.Log("Player disconnected!");
                break;
        }
    }
}

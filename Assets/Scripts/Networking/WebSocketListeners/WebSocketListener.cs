using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HybridWebSocket;

public class WebSocketListener
{

    public void HandlePacketData(byte[] bytes)
    {
        ReceivablePacket receivablePacket = new ReceivablePacket(bytes);

        ReceivablePacketManager.handle(receivablePacket);
    }

}


using System;

public class ReceivablePacketManager
{

    private static readonly DateTime Jan1st1970 = new DateTime
        (1970, 1, 1, 0, 0, 0, DateTimeKind.Local);

    public static long CurrentTimeMillis()
    {
        return (long)(DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
    }

    public static void handle(ReceivablePacket packet)
    {
        UnityEngine.Debug.Log("Packet received id: " + packet.ReadShort());

        NetworkManager.Instance.ChannelSend(new PingServer(packet.ReadLong()));
        switch (packet.ReadShort())
        {
            case 0:
                // AccountAuthenticationResult.notify(packet);
                break;
        }
    }
}

using System.IO;
using UnityEngine;

[PacketId(7)]
public class PacketInPlayerDamageOwn : PacketIn
{

    public PacketInPlayerDamageOwn() : base(true)
    {
    }

    public override void HandleData(MemoryStream ms)
    {
        float damage = (float) ReadDouble(ms);
        float playerHealth = (float) ReadDouble(ms);

        float damagePositionX = (float) ReadDouble(ms);
        float damagePositionY = (float) ReadDouble(ms);

        Vector2 damagePosition = new Vector2(damagePositionX, damagePositionY);

        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            ClientManager.Instance.DamageOwnPlayer(damage, playerHealth, damagePosition);
        });
    }
}

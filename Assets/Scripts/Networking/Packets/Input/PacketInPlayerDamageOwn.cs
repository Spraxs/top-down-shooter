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

        float damageDirectionX = (float) ReadDouble(ms);
        float damageDirectionY = (float) ReadDouble(ms);

        Vector2 damageDirection = new Vector2(damageDirectionX, damageDirectionY);

        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            ClientManager.Instance.DamageOwnPlayer(damage, playerHealth, damageDirection);
        });
    }
}

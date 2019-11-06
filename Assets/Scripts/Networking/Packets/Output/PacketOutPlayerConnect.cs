using System;

public class PacketOutPlayerConnect : PacketOut
{

    public int level;

    public double speed;
    public double damage;

    public long timeInMillis;

    private static readonly DateTime Jan1st1970 = new DateTime
    (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public static long CurrentTimeMillis()
    {
        return (long)(DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
    }

    public override void onDataPrepare()
    {
        id = 0; // TODO set this value with attribute

        level = 5;

        speed = 1.0d;
        damage = 2.5d;

        timeInMillis = CurrentTimeMillis();
    }
}

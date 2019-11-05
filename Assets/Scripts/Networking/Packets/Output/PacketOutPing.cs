
public class PacketOutPing : PacketOut
{
    public long timeInMillis;

    public PacketOutPing(long _timeInMillis)
    {
        timeInMillis = _timeInMillis;
    }

    public override void onDataPrepare()
    {
        id = 1; // TODO set this value with attribute
    }
}

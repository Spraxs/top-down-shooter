public class PacketOutPlayerHandTransformChange : PacketOut
{

    public double posX;
    public double posY;
    
    public double rotZ;

    public double scaleX;

    public PacketOutPlayerHandTransformChange(double _posX, double _posY, double _rotZ, double _scaleX)
    {
        posX = _posX;
        posY = _posY;

        rotZ = _rotZ;

        scaleX = _scaleX;
    }

    public override void onDataPrepare()
    {
        id = 7;
    }
}
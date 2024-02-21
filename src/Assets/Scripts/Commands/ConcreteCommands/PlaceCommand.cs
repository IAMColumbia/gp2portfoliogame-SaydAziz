public class PlaceCommand : ICommand
{
    float locX, locY, locZ;    
    public PlaceCommand(float x, float y, float z)
    {
        locX = x; locY = y; locZ = z;
    }

    public void Execute()
    {
        GameManager.Instance.PlaceTrinket(0, locX, locY, locZ);
    }
}

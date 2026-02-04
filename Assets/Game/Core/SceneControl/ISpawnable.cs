namespace Game.Core.SceneControl
{
    public enum SpawnSize
    {
        Small,
        Medium,
        Large
    }
    public interface ISpawnable
    {
        SpawnSize GetFixedSize();
    }

}


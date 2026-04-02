using System.Threading.Tasks;

namespace Game.Core.SceneControl.Spawnables.Hotel
{
    public interface IModule
    {
        Task InitializeModule(GameModeController controller);
        string GetModuleName();

    }

}

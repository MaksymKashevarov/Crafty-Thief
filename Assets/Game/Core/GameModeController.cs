using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Game.Core.SceneControl;
using Game.Core.SceneControl.Spawnables.Hotel;
using Game.Core.ServiceLocating;
using Game.Utility;

namespace Game.Core
{

    public enum GameMode
    {
        hotel,
        undefined,
    }
    public class GameModeController
    {
        private GlobalDriver _globalDriver;
        private SceneController _sceneController;
        List<IModule> _hotelModules = new List<IModule>();
        public GameModeController(GlobalDriver globalDriver, SceneController sceneController)
        {
            _globalDriver = globalDriver;
            _sceneController = sceneController;
        }

        public Task DefineGameMode(SceneData sceneData)
        {
            GameMode gameMode = sceneData.GetGameMode();
            switch (gameMode)
            {
                case GameMode.hotel:
                    Registry.hotelRegistry.Resolve(_hotelModules);
                    if (_hotelModules.Count == 0)
                    {
                        DevLog.LogAssetion("No hotel modules found for hotel game mode", this);
                        break;
                    }
                    break;
            }
            foreach (IModule module in _hotelModules)
            {
                DevLog.Log("Initializing hotel module: " + module.GetType().Name, this);
                DevLog.Log("Module name: " + module.GetModuleName(), this);
            }

            return Task.CompletedTask;
        }

    }

}
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
        private SceneData _menuScene;
        List<IModule> _hotelModules = new List<IModule>();
        List<IHotelRoomModule> _hotelRoomModules = new List<IHotelRoomModule>();
        public GameModeController(GlobalDriver globalDriver, SceneController sceneController)
        {
            _globalDriver = globalDriver;
            _sceneController = sceneController;
        }

        public void Initialize(SceneData _level)
        {
            _menuScene = _level;
            if (_menuScene == null)
            {
                DevLog.LogAssetion("Menu scene data is null. Please assign a valid SceneData asset to the GameModeController.", this);
                return;
            }
            DevLog.Log("Menu: " + _menuScene.GetSceneName(), this);

        }

        public Task DefineGameMode(SceneData sceneData)
        {
            GameMode gameMode = sceneData.GetGameMode();
            switch (gameMode)
            {
                case GameMode.hotel:
                    Registry.hotelRegistry.Resolve(_hotelModules);
                    //_hotelRoomModules = AssetTransformer.ConvertHotelRoomsAsync(AssetType.hotel_room).Result;
                    if (_hotelModules.Count == 0)
                    {
                        DevLog.LogAssetion("No hotel modules found for hotel game mode", this);
                        break;
                    }
                    foreach (IModule module in _hotelModules)
                    {
                        DevLog.Log("Initializing hotel module: " + module.GetType().Name, this);
                        DevLog.Log("Module name: " + module.GetModuleName(), this);
                        module.InitializeModule();
                    }
                    break;
            }

            return Task.CompletedTask;
        }

    }

}
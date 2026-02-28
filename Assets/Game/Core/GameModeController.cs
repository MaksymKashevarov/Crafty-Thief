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
        List<IHotelRoomModule> _utilityRooms = new List<IHotelRoomModule>();
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
                DevLog.LogAssertion("Menu scene data is null. Please assign a valid SceneData asset to the GameModeController.", this);
                return;
            }
            DevLog.Log("Menu: " + _menuScene.GetSceneName(), this);

        }

        public void GetHotelRoomModules(List<IHotelRoomModule> roomModules)
        {
            if (_hotelRoomModules.Count == 0)
            {
                DevLog.LogAssertion("No hotel room modules found. Please ensure that hotel room modules are properly defined and assigned.", this);
                return;
            }
            foreach (IHotelRoomModule module in _hotelRoomModules)
            {
                roomModules.Add(module);
            }
        }

        public void GetUtilityRooms(List<IHotelRoomModule> utilityRooms)
        {
            if (_utilityRooms.Count == 0)
            {
                DevLog.LogWarning("No utility rooms found. This may be intentional if your game design does not include utility rooms, but please verify that this is the expected behavior.", this);
                return;
            }
            foreach (IHotelRoomModule module in _utilityRooms)
            {
                utilityRooms.Add(module);
            }
        }

        public Task DefineGameMode(SceneData sceneData)
        {
            GameMode gameMode = sceneData.GetGameMode();
            switch (sceneData.GetGameMode())
            {
                case GameMode.hotel:
                    return DefineHotelMode(sceneData);

                default:
                    return Task.CompletedTask;
            }
        }

        private async Task DefineHotelMode(SceneData sceneData)
        {
            Registry.hotelRegistry.Resolve(_hotelModules);
            _hotelRoomModules = await AssetTransformer.ConvertHotelRoomsAsync(AssetType.hotel_room);

            if (_hotelRoomModules.Count == 0)
            {
                DevLog.LogAssertion("No hotel room modules found for hotel game mode", this);
                return;
            }

            DevLog.Log("Hotel room modules found: " + _hotelRoomModules.Count, this);

            if (_hotelModules.Count == 0)
            {
                DevLog.LogAssertion("No hotel modules found for hotel game mode", this);
                return;
            }

            foreach (IModule module in _hotelModules)
            {
                DevLog.Log("Initializing hotel module: " + module.GetModuleName(), this);
                module.InitializeModule(this);
            }

            Registry.hotelRegistry.ResolveUtilityModules(_utilityRooms);

            if (_utilityRooms.Count == 0)
            {
                DevLog.LogWarning("No utility rooms found for hotel game mode. This may be intentional if your game design does not include utility rooms, but please verify that this is the expected behavior.", this);
            }

        }

    }

}
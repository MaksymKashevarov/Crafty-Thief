using Game.Core.UI;

namespace Game.Core.Profile
{
    public class UIProfileController
    {
        private PlayerInterface _playerInterface;
        private UIController _uiController;
        private PlayerProfile _playerProfile;
        public UIProfileController(PlayerInterface playerInterface, UIController controller)
        {
            DevLog.Log("Initializing Profile Controller....", this);
            this._playerInterface = playerInterface;
            this._uiController = controller;
        }

        public void Assemble()
        {
            _playerProfile = _playerInterface.RequestPlayerProfile();
        }

    }

}

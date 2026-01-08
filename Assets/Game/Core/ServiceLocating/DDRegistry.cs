using Game.Core.UI;
using UnityEngine;

namespace Game.Core.ServiceLocating
{

    public class DDRegistry
    {
        private DifficultyDisplayButton _ddButton;

        public void RegisterDifficultyButton(DifficultyDisplayButton button)
        {
            _ddButton = button;
        }

        public DifficultyDisplayButton ResolveDifficultyButton()
        {
            if (_ddButton == null)
            {
                Debug.LogAssertion("No Button Set");
                return null;
            }
            return _ddButton;
        }
    }

}


namespace Game.Core.UI
{
    using Game.Core.Interactable;
    using Game.Core.Player;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class UIController : MonoBehaviour
    {
        [SerializeField] private Transform _slotsParent;
        private GameObject _currentInterface;
        private PlayerInterface _currentPlayerInterface;


        public void SetCurrentInterace(GameObject element)
        {
            _currentInterface = element;
        }

        public GameObject GetCurrentInterface()
        {
            if (_currentInterface == null)
            {
                Debug.LogWarning("No current element present!");
                return null;
            }
            else
            {
                return _currentInterface;
            }
        }

        public void SetPlayerInterface(PlayerInterface playerInterface)
        {
           _currentPlayerInterface = playerInterface;
        }


        public void DestroyElement(GameObject element)
        {
            if (element != null)
            {
                Debug.Log($"Element {element} destroyed");
                Destroy(element);
            }
            else
            {
                Debug.LogWarning("Element is missing");
            }
        }

        public void ShowElement(GameObject element, Transform parent, IUIElement source)
        {
            if (element == null) return;

            _currentPlayerInterface.ClearElements(source);

            GameObject instance = Instantiate(element, parent);
            IUIElement instanceElement = instance.GetComponent<IUIElement>();

            if (instanceElement != null)
            {
                instanceElement.SetController(this);
                source.SetActiveElement(instance);
                instanceElement.SetRootElement(source);
            }
            else
            {
                Debug.LogWarning("Component cannot be found!");
            }
        }

        public void TerminateCurrentInterface()
        {
            if (_currentInterface != null)
            {
                DestroyElement(_currentInterface);
                _currentInterface = null;
                Debug.Log("Success");
            }
            else
            {
                Debug.LogError("No interface present!");
            }
        }

        public void OpenInventoryInterface()
        {
            
        }
    }

}


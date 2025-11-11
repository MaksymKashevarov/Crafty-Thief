namespace Game.Core.UI
{
    using Game.Core.Interactable;
    using Game.Core.Player;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class UIController : MonoBehaviour
    {
        [SerializeField] private Image _itemSlot;
        [SerializeField] private Sprite _slotDefaultImage;
        [SerializeField] private GameObject _storageInterface;
        [SerializeField] private GameObject _storageSlot;
        [SerializeField] private Transform _slotsParent;
        private GameObject _currentInterface;
        private PlayerInterface _currentPlayerInterface;


        public void SetImage(Sprite image, Image destination = null)
        {
            if (destination != null)
            {
                destination.sprite = image;
            }
            else
            {
                _itemSlot.sprite = image;
            }
            
        }


        public void SetPlayerInterface(PlayerInterface playerInterface)
        {
           _currentPlayerInterface = playerInterface;
        }


        public List<IUIElement> AddStorageSlot(List<IUIElement> storageList)
        {
            if(_currentInterface != null && _slotsParent != null)
            {
                GameObject spawnPrefab = Instantiate(_storageSlot, _slotsParent);
                IUIElement uiElement = spawnPrefab.GetComponent<IUIElement>();
                if (uiElement != null)
                {
                    storageList.Add(uiElement);
                    uiElement.SetController(this);
                    Debug.Log("Element has been added");
                    return storageList;
                }
                else
                {
                    Debug.LogWarning("EXCEPTION");
                    Destroy(spawnPrefab);
                    return null;
                }
            }
            else
            {
                return null;
            }           
        }

        public void RequestPlacing(IUIElement source)
        {
            Item item = source.getSlotItem();
            if (item != null)
            {
                Debug.LogWarning("There is already an item in storage");
            }
            else
            {
                Image sourceImg = source.GetSlotImage();
                _currentPlayerInterface.PlaceItemInStorage(source, item, sourceImg);
            }
            GameObject tmpElement = source.GetActiveElement();
            DestroyElement(tmpElement);
        }

        public void RequestTaking(IUIElement source)
        {
            Item item = source.getSlotItem();
            Image sourceImg = source.GetSlotImage();
            if (item != null)
            {
                _currentPlayerInterface.TakeItemFromStorage(source, item, sourceImg);
                source.SetSlotItem(null);
            }
            else
            {
                Debug.LogWarning("Request Denied");
            }
            GameObject tmpElement = source.GetActiveElement();
            DestroyElement(tmpElement);
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

        public void SetDefaultImage(Image destination = null)
        {
            if (destination != null)
            {
                destination.sprite = _slotDefaultImage;
            }
            else
            {
                _itemSlot.sprite = _slotDefaultImage;
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

        public void OpenStorageInterface()
        {
            _currentInterface = Instantiate(_storageInterface, transform);
            var storageInterface = _currentInterface.GetComponent<StorageInterfaceView>();
            if (storageInterface != null) 
            { 
                _slotsParent = storageInterface.GetSlotsParent();
            }
        }
    }

}


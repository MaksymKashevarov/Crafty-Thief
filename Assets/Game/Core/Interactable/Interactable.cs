namespace Game.Core.Interactable
{
    using Game.Core.DI;
    using Game.Core.Player;
    using UnityEngine;

    public class Interactable : MonoBehaviour, IInteractable
    {
        [SerializeField] private Item _item;
        [SerializeField] private bool _isInteractable;
        [SerializeField] private bool _isValuable;

        public Item GetItem()
        {
            return _item;
        }

        private void Awake()
        {
            SpawnRegistry.itemRegistry.Add(this);
        }

        public void SetValuable(bool flag)
        {
            _isValuable = flag;
        }

        public bool GetValuable()
        {
            return _isValuable;
        }

        public void Interact(Hands hands)
        {
            if (_isValuable)
            {
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Not Main Item");
            }
        }

        public bool IsInteractable()
        {
            if (_isInteractable)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool isTool()
        {
            return false;
        }
    }

}


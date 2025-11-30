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
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private bool _isAttached = false;

        public Item GetItem()
        {
            return _item;
        }

        public Rigidbody GetRigidbody()
        {
            if (_rigidbody == null)
            {
                Debug.LogWarning("RigidBody is Missing!");
            }
            return _rigidbody;
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        private void Awake()
        {
            Registry.itemRegistry.Add(this);
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
                Anchor playerAnchor = hands.GetAnchor();
                if (playerAnchor != null && !_isAttached)
                {
                    playerAnchor.Attach(this, _isAttached);
                    _isAttached = true;
                }
                else
                {
                    playerAnchor.Detatch();
                    _isAttached = false;
                }
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


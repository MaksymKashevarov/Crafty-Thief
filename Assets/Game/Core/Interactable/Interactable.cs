namespace Game.Core.Interactable
{
    using UnityEngine;

    public class Interactable : MonoBehaviour, IInteractable
    {
        [SerializeField] private Item _item;
        [SerializeField] private bool _isInteractable;

        public void Start()
        {      
        }

        public Item GetItem()
        {
            return _item;
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


    }

}


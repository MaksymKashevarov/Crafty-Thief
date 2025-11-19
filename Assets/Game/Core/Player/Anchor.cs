namespace Game.Core.Player
{
    using Game.Core.Interactable;
    using UnityEngine;

    public class Anchor : MonoBehaviour
    {
        private GameObject _currentObject; 
        private GameObject _activeObject; 
        private Rigidbody _activeRb;
        private float _snapRadius = 0.45f;
        [SerializeField] private bool _activeFlag;
        public void Attach(IInteractable obj, bool flag)
        {
            if (_currentObject != null)
            {
                Debug.LogWarning("Already Holding Item");
                return;
            }

            if (flag)
            {
                Debug.LogWarning("Item Flag is Invalid");
            }

            _activeObject = obj.GetGameObject();
            _activeRb = obj.GetRigidbody();

            if (_activeRb == null)
            {
                Debug.LogWarning("Missing RB");
                return;
            }

            _activeRb.useGravity = false;
            _activeRb.linearVelocity = Vector3.zero;
            _activeRb.angularVelocity = Vector3.zero;
            _activeRb.isKinematic = true;
            _activeFlag = flag;
            _activeFlag = true;

        }

        public void Detatch()
        {
            if (_currentObject == null)
            {
                Debug.LogWarning("Nothing to detatch");
                return;
            }
            if (!_activeFlag)
            {
                Debug.LogWarning("Item Flag is Invalid");
            }
            _activeRb.useGravity = true;
            _activeRb.isKinematic = false;
            _currentObject.transform.SetParent(null);
            _currentObject = null;
            _activeRb = null;
            _activeFlag = false;

        }

        public bool IsHoldingItem()
        {
            if (_currentObject != null)
            {
                return true;
            }
            return false;
        }


        private void Update()
        {
            if (_currentObject == null && _activeObject != null)
            {
                _activeObject.transform.position = Vector3.Lerp(_activeObject.transform.position, transform.position, Time.deltaTime * 10f);

                if (Vector3.Distance(_activeObject.transform.position, transform.position) < _snapRadius)
                {
                    _activeObject.transform.SetParent(transform);
                    _activeObject.transform.localPosition = Vector3.zero;

                    _currentObject = _activeObject;  
                    _activeObject = null;            
                }
            }
        }

    }

}


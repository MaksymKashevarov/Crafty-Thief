namespace Game.Core.Player
{
    using Game.Core.Interactable;
    using UnityEngine;

    public class Anchor : MonoBehaviour
    {
        private GameObject _currentObject; 
        private GameObject _activeObject; 
        private Rigidbody _activeRb;
        public void Attach(IInteractable obj, bool mode)
        {
            if (mode && _currentObject == null)
            {
                _activeObject = obj.GetGameObject();
                _activeRb = obj.GetRigidbody();

                if (_activeRb != null)
                {
                    _activeRb.useGravity = false;
                    _activeRb.linearVelocity = Vector3.zero;
                    _activeRb.angularVelocity = Vector3.zero;
                    _activeRb.isKinematic = true;
                }
                else
                {
                    return;
                }
            }
            else
            {
                if (_activeRb != null)
                {
                    _activeRb.useGravity = true;
                    _activeRb.isKinematic = false;
                }
                _currentObject.transform.SetParent(null);
                _currentObject = null;
                _activeRb = null;;
                Debug.Log("Dropping");
            }
        }


        private void Update()
        {
            if (_currentObject == null && _activeObject != null)
            {
                _activeObject.transform.position = Vector3.Lerp(_activeObject.transform.position, transform.position, Time.deltaTime * 10f);

                if (Vector3.Distance(_activeObject.transform.position, transform.position) < 0.05f)
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


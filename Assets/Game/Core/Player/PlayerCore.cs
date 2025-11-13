namespace Game.Core.Player
{
    using Game.Attributes;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using Game.Core.Interactable;
    using Game.Core.UI;

    public class PlayerCore : MonoBehaviour
    {
        [SerializeField] private CharacterController _cc;
        [SerializeField] private Transform _cameraPivot;
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _lookSensitivity = 200f;
        [SerializeField] private NpcStats _stats;
        [SerializeField] private float _interactDistance = 3f;
        [SerializeField] private LayerMask _interactMask;
        [SerializeField] private UIController _controller;
        [SerializeField] private GlobalDriver _globalDriver;
        [SerializeField] private int _handsInventorySize;
        private Dictionary<string, float> stats = new();
        private Hands _hands;
        private PlayerInterface _playerInterface;
        private bool IsInteracting;

        private Vector2 _move;
        private Vector2 _look;
        private float _pitch;

        public void SetInteraction(bool flag)
        {
            IsInteracting = flag;
        }

        public bool isCurrentlyInteracting()
        {
            return IsInteracting;
        }

        private void OnMove(InputValue value)
        {
            _move = value.Get<Vector2>();
        }

        private void OnLook(InputValue value)
        {
            _look = value.Get<Vector2>();
        }

        private void OnTerminate(InputValue value)
        {
            if (value.isPressed == false)
            {
                return;
            }
            Debug.Log("Check");
            _playerInterface.TerminateInterface();
        }

        private void OnOpenInventory(InputValue value)
        {
            if (value.isPressed == false)
            {
                return;
            }
        }

        public void SetController(UIController controller)
        {
            _controller = controller;
        }

        public void SetDriver(GlobalDriver driver)
        {
            _globalDriver = driver;
        }

        private void OnInteract(InputValue value)
        {
            if (value.isPressed == false)
            {
                return;
            }

            if (_cameraPivot == null)
            {
                Debug.LogWarning("Interact: _cameraPivot == null");
                return;
            }

            Ray ray = new Ray(_cameraPivot.position, _cameraPivot.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, _interactDistance, _interactMask))
            {
                Debug.Log("Interact: Ray hit " + hit.collider.name);
                GameObject item = hit.collider.gameObject;
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();

                if (interactable != null && interactable.IsInteractable())
                {
                    bool isInventoryFull = _playerInterface.IsInventoryFull();
                    if (isInventoryFull)
                    {
                        Debug.LogWarning("Inventory is Full!");
                        return;
                    }
                    else
                    {
                        Debug.Log("Interact: IInteractable found and is interactable");
                        _hands.Pickup(interactable, item);
                    }                                                        
                }
            }
        }
        private void Start()
        {
            if (_hands == null)
            {
                _hands = new(_cameraPivot, this);
            }
            if (_playerInterface == null && _controller != null)
            {
                _playerInterface = new(this, _hands, _controller, _globalDriver);
            }
            else
            {
                Debug.LogError("COMPONENT Missing!");
            }
            _globalDriver.SetActivePlayerInterface(_playerInterface);
            _playerInterface.SetInventorySize(_handsInventorySize);
            _controller.SetPlayerInterface(_playerInterface);
            
        }

        private void BuildCharacter()
        {
            foreach (var entry in _stats.CharacterData)
            {
                stats[entry.Name] = entry.Value;
            }
        }

        private void LookUpdate()
        {
            Vector3 right = transform.right * _move.x;
            Vector3 forward = transform.forward * _move.y;
            Vector3 dir = right + forward;

            _cc.SimpleMove(dir * _moveSpeed);

            float yawDelta = _look.x * _lookSensitivity * Time.deltaTime / 100f;
            float pitchDelta = _look.y * _lookSensitivity * Time.deltaTime / 100f;

            _pitch = _pitch - pitchDelta;
            _pitch = Mathf.Clamp(_pitch, -80f, 80f);

            if (_cameraPivot != null)
            {
                _cameraPivot.localRotation = Quaternion.Euler(_pitch, 0f, 0f);
            }

            transform.Rotate(0f, yawDelta, 0f);
        }

        private void Update()
        {
            if (!isCurrentlyInteracting())
            {
                LookUpdate();
                _playerInterface.DrawCursor(false);
            }
            _playerInterface.Refresh();
        }

    }
}

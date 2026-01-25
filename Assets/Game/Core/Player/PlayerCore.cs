namespace Game.Core.Player
{
    using Game.Attributes;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using Game.Core.Interactable;
    using Game.Core.UI;
    using Game.Core.ServiceLocating;

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
        [SerializeField] private Anchor _anchor;
        private Dictionary<string, float> stats = new();
        private Hands _hands;
        private PlayerInterface _playerInterface;
        private bool IsInteracting;
        private bool _isPlayable;

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
        }

        private void OnOpenInventory(InputValue value)
        {
            if (value.isPressed == false)
            {
                return;
            }
        }

        private void OnInteract(InputValue value)
        {
            if (value.isPressed == false)
            {
                return;
            }
            if (!_isPlayable)
            {
                return;
            }

            if (_cameraPivot == null)
            {
                Debug.LogWarning("Interact: _cameraPivot == null");
                return;
            }

            if (_anchor != null && _anchor.IsHoldingItem())
            {
                _anchor.Detatch();
                return;
            }

            Ray ray = new Ray(_cameraPivot.position, _cameraPivot.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, _interactDistance, _interactMask))
            {
                Debug.Log("Interact: Ray hit " + hit.collider.name);
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();


                if (interactable != null && interactable.IsInteractable())
                {
                    if (_playerInterface.IsItemInList(interactable))
                    {
                        Debug.Log("Interact: IInteractable found and is interactable");
                        _hands.Interact(interactable);
                        _playerInterface.UpdateActiveList(interactable);
                        _globalDriver.CheckListCompletion();
                    }
                    else
                    {
                        _hands.Interact(interactable);
                    }
                }
            }
        }

        public void SetPlayable(bool value)
        {
            _isPlayable = value;
        }

        public void Initialize(UIController controller, GlobalDriver driver)
        {
            _controller = controller;
            _globalDriver = driver;
            if (_playerInterface != null)
            {
                Debug.LogError("Invalid Interface");
                return;
            }
            if (_hands == null)
            {
                if(_anchor == null)
                {
                    Debug.Log("Anchor is missing!");
                }
                Debug.Log("hands Install Success");
                _hands = new(_cameraPivot, this, _anchor);
            }
            if (_playerInterface == null)
            {
                if (_controller != null)
                {
                    _playerInterface = new(this, _hands, _controller, _globalDriver);
                }
                else
                {
                    Debug.LogError("CONTROLLER missing!");
                    return;
                }
            }
            if (_globalDriver != null && _controller != null)
            {
                if (_playerInterface == null)
                {
                    Debug.LogError("Interface is Missing");
                    return;
                }
                _controller.SetPlayerInterface(_playerInterface);
                _globalDriver.SetActivePlayerInterface(_playerInterface);
                Debug.Log("Initiating callback! Interface sent!");
            }
            _playerInterface.SetInventorySize(_handsInventorySize);
            Container.Register(this);
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
            if (_playerInterface == null)
            {
                Debug.LogWarning("Interface Missing!");
                return;
            }

            if (_isPlayable)
            {
                LookUpdate();
                _playerInterface.DrawCursor(false);
                return;
            }

            _playerInterface.DrawCursor(true);
            _playerInterface.Refresh();
        }

    }
}

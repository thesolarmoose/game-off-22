using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Utils.Extensions;
using Utils.Input;

namespace Movement
{
    public class OnClickDestinationSetter : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _destination;
        [SerializeField] private Transform _character;

        [SerializeField] private LayerMask _ignoreClicksOn;

        private UnityEvent _onMovementRequest;
        
        private List<RaycastResult> _raycasts;
        private InputAction _pointAction;
        private InputAction _clickAction;

        private bool _shouldMove;

        public async Task WaitForMovementRequest(CancellationToken ct)
        {
            bool requested = false;

            void OnRequest()
            {
                requested = true;
                _onMovementRequest.RemoveListener(OnRequest);
            }

            _onMovementRequest.AddListener(OnRequest);

            while (!requested && !ct.IsCancellationRequested)
            {
                await Task.Yield();
            }
        }
        
        private void Start()
        {
            _onMovementRequest = new UnityEvent();
            _raycasts = new List<RaycastResult>();
            _pointAction = InputActionUtils.GetPointAction();
            _clickAction = InputActionUtils.GetClickAction();
            _clickAction.performed += OnClick;

            _pointAction.Enable();
            _clickAction.Enable();
            
            // move to character position
            MoveToPosition(_character.position);
        }

        private void Update()
        {
            if (_shouldMove)
            {
                MoveToPointer();
            }
        }

        private void OnClick(InputAction.CallbackContext ctx)
        {
            var clickDown = _clickAction.ReadValue<float>() > 0.5f;
            _shouldMove = clickDown;
            
            if (clickDown)
            {
                var screenPosition = _pointAction.ReadValue<Vector2>();
                var ignore = ClickedOnIgnore(screenPosition);
                _shouldMove = !ignore;

                if (!ignore)
                {
                    var worldPosition = _camera.ScreenToWorldPoint(screenPosition);
                    MoveToPosition(worldPosition);
                    _onMovementRequest?.Invoke();
                }
            }
        }

        private void MoveToPointer()
        {
            var screenPosition = _pointAction.ReadValue<Vector2>();
            var worldPosition = _camera.ScreenToWorldPoint(screenPosition);
            MoveToPosition(worldPosition);
        }
        
        private void MoveToPosition(Vector2 worldPosition)
        {
            _destination.position = worldPosition;
        }

        private bool ClickedOnIgnore(Vector2 cursorPos)
        {
            var eventSystem = EventSystem.current;
            var eventData = new PointerEventData(eventSystem)
            {
                position = cursorPos
            };
            eventSystem.RaycastAll(eventData, _raycasts);
            if (_raycasts.Count > 0)
            {
                var first = _raycasts[0];
                bool ignore = _ignoreClicksOn.IsLayerInMask(first.gameObject.layer);
                return ignore;
            }

            return false;
        }

        private void OnEnable()
        {
            _pointAction?.Enable();
            _clickAction?.Enable();
        }
        
        private void OnDisable()
        {
            _pointAction?.Disable();
            _clickAction?.Disable();
        }
    }
}
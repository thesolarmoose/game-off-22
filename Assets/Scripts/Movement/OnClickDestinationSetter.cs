using System;
using System.Collections.Generic;
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

        [SerializeField] private LayerMask _ignoreClicksOn;

        private UnityEvent _onMovementRequest;
        
        private List<RaycastResult> _raycasts;
        private InputAction _pointAction;
        private InputAction _tapAction;

        public UnityEvent OnMovementRequest
        {
            get => _onMovementRequest;
            set => _onMovementRequest = value;
        }

        private void Start()
        {
            _onMovementRequest = new UnityEvent();
            _raycasts = new List<RaycastResult>();
            _pointAction = InputActionUtils.GetPointAction();
            _tapAction = InputActionUtils.GetTapAction();
            _tapAction.performed += OnTap;

            _pointAction.Enable();
            _tapAction.Enable();
        }

        private void OnTap(InputAction.CallbackContext ctx)
        {
            var screenPosition = _pointAction.ReadValue<Vector2>();
            var ignore = ClickedOnIgnore(screenPosition);

            if (!ignore)
            {
                var worldPosition = _camera.ScreenToWorldPoint(screenPosition);
                _destination.position = worldPosition;
                _onMovementRequest?.Invoke();
            }
        }

        private bool ClickedOnIgnore(Vector2 worldPosition)
        {
            var eventSystem = EventSystem.current;
            var eventData = new PointerEventData(eventSystem)
            {
                position = worldPosition
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
            _tapAction?.Enable();
        }
        
        private void OnDisable()
        {
            _pointAction?.Disable();
            _tapAction?.Disable();
        }
    }
}
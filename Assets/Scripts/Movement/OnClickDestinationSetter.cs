using UnityEngine;
using UnityEngine.InputSystem;
using Utils.Input;

namespace Movement
{
    public class OnClickDestinationSetter : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _destination;
        
        private InputAction _pointAction;
        private InputAction _tapAction;

        private void Start()
        {
            _pointAction = InputActionUtils.GetPointAction();
            _tapAction = InputActionUtils.GetTapAction();
            _tapAction.performed += OnTap;

            _pointAction.Enable();
            _tapAction.Enable();
        }

        private void OnTap(InputAction.CallbackContext ctx)
        {
            var screenPosition = _pointAction.ReadValue<Vector2>();
            var worldPosition = _camera.ScreenToWorldPoint(screenPosition);
            _destination.position = worldPosition;
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
using System.Threading;
using System.Threading.Tasks;
using AsyncUtils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace UI
{
    public class PauseMenuLauncher : MonoBehaviour
    {
        [SerializeField] private AsyncPopup _pauseMenuPopup;
        [SerializeField] private InputAction _pauseAction;

        [SerializeField] private UnityEvent _onBeforePause;
        [SerializeField] private UnityEvent _onAfterPause;

        private CancellationTokenSource _cts;
        private bool _paused;

        private async void ShowPauseMenu(InputAction.CallbackContext ctx)
        {
            if (!_paused)
            {
                _paused = true;
                _onBeforePause.Invoke();
                _cts = new CancellationTokenSource();
                var ct = _cts.Token;
                await Popups.ShowPopup(_pauseMenuPopup, ct);    
                _paused = false;
                _onAfterPause.Invoke();
            }
            else
            {
                await Task.Yield();
                Cancel();
            }
        }

        private void Start()
        {
            _pauseAction.Enable();
            _pauseAction.performed += ShowPauseMenu;
        }

        private void OnEnable()
        {
            _pauseAction?.Enable();
        }

        private void OnDisable()
        {
            Cancel();
            _pauseAction?.Disable();
        }

        private void Cancel()
        {
            if (!_cts.IsCancellationRequested)
            {
                _cts.Cancel();
            }

            _cts.Dispose();
        }
    }
}
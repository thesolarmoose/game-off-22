using System.Collections.Generic;
using System.Threading;
using TNRD;
using UnityEngine;
using UnityEngine.Events;

namespace Interactions
{
    public class InteractableEvent : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onStartInteraction;
        [SerializeField] private UnityEvent _onFinishInteraction;

        [SerializeField] private List<SerializableInterface<IInteractionEvent>> _events;

        private CancellationTokenSource _cts;
        private bool _isRunning;

        private void OnEnable()
        {
            _cts = new CancellationTokenSource();
        }

        private void OnDisable()
        {
            if (!_cts.IsCancellationRequested)
            {
                _cts.Cancel();
            }
            
            _cts.Dispose();
        }

        public void StartInteraction()
        {
            if (!_isRunning)
            {
                _isRunning = true;
                _onStartInteraction.Invoke();
                var ct = _cts.Token;
                ExecuteEvents(ct);
            }
        }

        private async void ExecuteEvents(CancellationToken ct)
        {
            foreach (var @event in _events)
            {
                var shouldContinue = await @event.Value.ExecuteEvent(ct);
                if (!shouldContinue)
                {
                    break;
                }
            }

            _onFinishInteraction.Invoke();
            _isRunning = false;
        }
    }
}
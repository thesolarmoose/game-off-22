using System;
using System.Collections.Generic;
using System.Threading;
using Items;
using UnityEngine;

namespace Interactions
{
    public class EventsExecutionController : MonoBehaviour
    {
        private CancellationTokenSource _cts;

        private void Start()
        {
            _cts = new CancellationTokenSource();
        }

        private void OnDisable()
        {
            CancelEvents();
        }

        private void CancelEvents()
        {
            if (!_cts.IsCancellationRequested)
            {
                _cts.Cancel();
            }

            _cts.Dispose();
        }

        public void ExecuteEvents(InteractableEvent evt, Item item)
        {
            CancelEvents();
            _cts = new CancellationTokenSource();
            var ct = _cts.Token;
            ExecuteEvents(evt, item, ct);
        }
        
        private async void ExecuteEvents(InteractableEvent evt, Item item, CancellationToken ct)
        {
            await evt.ExecuteEvents(item, ct);
        }
    }
}
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Interactions.Events
{
    [Serializable]
    public class EventUnityEvent : IInteractionEvent
    {
        [SerializeField] private UnityEvent _event;
        
        public async Task<bool> ExecuteEvent(CancellationToken ct)
        {
            _event.Invoke();

            return true;
        }
    }
}
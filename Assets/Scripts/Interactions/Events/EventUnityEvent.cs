using System;
using System.Threading;
using System.Threading.Tasks;
using Items;
using UnityEngine;
using UnityEngine.Events;

namespace Interactions.Events
{
    [Serializable]
    public class EventUnityEvent : IInteractionEvent
    {
        [SerializeField] private UnityEvent _event;
        
        public async Task<bool> ExecuteEvent(Item item, CancellationToken ct)
        {
            _event.Invoke();

            await Task.Yield();

            return true;
        }

        public static EventUnityEvent Create(Action action)
        {
            var evt = new EventUnityEvent()
            {
                _event = new UnityEvent()
            };
            evt._event.AddListener(action.Invoke);
            return evt;
        }
    }
}
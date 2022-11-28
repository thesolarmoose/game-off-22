using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Items;
using TNRD;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Interactions
{
    public class InteractableEvent : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onStartEvent;
        [SerializeField] private UnityEvent _onFinishEvent;

        [SerializeField] private InteractableEventEvent _onInteractedEvent;

        [SerializeField] private List<SerializableInterface<IInteractionEvent>> _events;

        public void NotifyInteraction(BaseEventData data)
        {
            if (data is PointerEventData p)
            {
                if (p.button == PointerEventData.InputButton.Left)
                {
                    _onInteractedEvent.Raise(this);
                }
            }
        }

        public async Task ExecuteEvents(Item item, CancellationToken ct)
        {
            _onStartEvent.Invoke();

            var events = _events.ConvertAll(evt => evt.Value);
            await ExecuteEvents(item, events, ct);

            _onFinishEvent.Invoke();
        }

        public static async Task<bool> ExecuteEvents(Item item, List<IInteractionEvent> events, CancellationToken ct)
        {
            bool shouldContinue = true;
            foreach (var @event in events)
            {
                shouldContinue = await @event.ExecuteEvent(item, ct);
                if (!shouldContinue)
                {
                    break;
                }
            }
            
            return shouldContinue;
        }
    }
}
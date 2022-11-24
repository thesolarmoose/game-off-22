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
            
            foreach (var @event in _events)
            {
                var shouldContinue = await @event.Value.ExecuteEvent(item, ct);
                if (!shouldContinue)
                {
                    break;
                }
            }

            _onFinishEvent.Invoke();
        }
    }
}
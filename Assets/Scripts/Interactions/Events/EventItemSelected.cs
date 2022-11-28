using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Items;
using TNRD;
using UnityEngine;
using Utils.Attributes;

namespace Interactions.Events
{
    [Serializable]
    public class EventItemSelected : IInteractionEvent
    {
        [SerializeField, AutoProperty(AutoPropertyMode.Scene)] private InventoryUi _inventory;
        [SerializeField] private List<SerializableInterface<IInteractionEvent>> _noItemSelectedEvents;
        [SerializeField] private List<SerializableInterface<IInteractionEvent>> _wrongItemSelectedEvents;
        [SerializeField] private List<ItemSelectedEvent> _itemSelectedEvents;
        
        public async Task<bool> ExecuteEvent(Item item, CancellationToken ct)
        {
            var events = GetValidEvents(item);
            
            bool shouldContinue = await InteractableEvent.ExecuteEvents(item, events, ct);
            return shouldContinue;
        }

        private List<IInteractionEvent> GetValidEvents(Item item)
        {
            var serializableEvents = _noItemSelectedEvents;
            
            if (item != null)
            {
                var exists = _itemSelectedEvents.Exists(itemEvent => itemEvent.EvaluateItem(item));
                if (exists)
                {
                    var @event = _itemSelectedEvents.First(itemEvent => itemEvent.EvaluateItem(item));
                    serializableEvents = @event.Events;

                    var serializedEvent = new SerializableInterface<IInteractionEvent>()
                    {
                        Value = EventUnityEvent.Create(() =>
                        {
                            FMODUnity.RuntimeManager.CreateInstance(item.UseSound).start();
                        })
                    };
                    serializableEvents.Insert(0, serializedEvent);
                }
                else
                {
                    serializableEvents = _wrongItemSelectedEvents;
                }
            }

            var events = serializableEvents.ConvertAll(serializableEvent => serializableEvent.Value);
            return events;
        }
    }

    [Serializable]
    public class ItemSelectedEvent
    {
        [SerializeField] private Item _item;
        [SerializeField] private List<SerializableInterface<IInteractionEvent>> _events;

        public List<SerializableInterface<IInteractionEvent>> Events => _events;

        public bool EvaluateItem(Item item)
        {
            return item == _item;
        }
    }
}
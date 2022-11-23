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
        
        public async Task<bool> ExecuteEvent(CancellationToken ct)
        {
            var events = GetAppropriateEvent();
            bool shouldContinue = true;
            foreach (var @event in events)
            {
                shouldContinue = await @event.ExecuteEvent(ct);
                if (!shouldContinue)
                {
                    break;
                }
            }

            return shouldContinue;
        }

        private List<IInteractionEvent> GetAppropriateEvent()
        {
            bool thereIsItemSelected = _inventory.ExistsItemSelected();
            var serializableEvents = _noItemSelectedEvents;
            
            if (thereIsItemSelected)
            {
                var selectedItem = _inventory.GetSelectedItem();
                var exists = _itemSelectedEvents.Exists(itemEvent => itemEvent.EvaluateItem(selectedItem));
                if (exists)
                {
                    var @event = _itemSelectedEvents.First(itemEvent => itemEvent.EvaluateItem(selectedItem));
                    serializableEvents = @event.Events;
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
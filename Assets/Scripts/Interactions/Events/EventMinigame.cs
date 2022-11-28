using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AsyncUtils;
using Items;
using TNRD;
using UnityEngine;

namespace Interactions.Events
{
    public class EventMinigame : MonoBehaviour, IInteractionEvent
    {
        [SerializeField] private Minigame.Minigame _minigamePrefab;
        [SerializeField] private List<SerializableInterface<IInteractionEvent>> _winEvents;
        [SerializeField] private List<SerializableInterface<IInteractionEvent>> _loseEvents;
        
        public async Task<bool> ExecuteEvent(Item item, CancellationToken ct)
        {
            var won = await Popups.ShowPopup(_minigamePrefab, ct);

            var serializedEvents = won ? _winEvents : _loseEvents;
            var events = serializedEvents.ConvertAll(evt => evt.Value);
            return await InteractableEvent.ExecuteEvents(item, events, ct);
        }
    }
}
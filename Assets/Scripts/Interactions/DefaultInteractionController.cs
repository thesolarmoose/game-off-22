using Items;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace Interactions
{
    public class DefaultInteractionController : MonoBehaviour
    {
        [SerializeField] private EventsExecutionController _controller;
        [SerializeField] private InteractableEventEvent _interactionEvent;
        [SerializeField] private InventoryUi _inventory;

        private void OnEnable()
        {
            _interactionEvent.Register(OnInteraction);
        }

        private void OnDisable()
        {
            _interactionEvent.Unregister(OnInteraction);
        }

        private void OnInteraction(InteractableEvent interactable)
        {
            Item item = null;
            if (_inventory.IsOpened)
            {
                item = _inventory.CurrentItemSelected;
            }
            
            _controller.ExecuteEvents(interactable, item);
        }
    }
}
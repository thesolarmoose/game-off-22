using System.Threading;
using System.Threading.Tasks;
using AsyncUtils;
using Dialogues.Core;
using Dialogues.UI;
using Items;
using Movement;
using UnityEngine;
using Utils.Attributes;

namespace Interactions.Events
{
    public class EventDisplayDialogue : MonoBehaviour, IInteractionEvent
    {
        [SerializeField, AutoProperty(AutoPropertyMode.Scene)]
        private OnClickDestinationSetter _clickMovementController;

        [SerializeField] private GameObject _inventoryUi;

        [SerializeField] private Dialogue _dialogue;
        [SerializeField] private DialoguePopup _dialoguePopupPrefab;
        
        public async Task<bool> ExecuteEvent(Item item, CancellationToken ct)
        {
            _clickMovementController.enabled = false;
            _inventoryUi.SetActive(false);

            await Popups.ShowPopup(_dialoguePopupPrefab, _dialogue, ct);
            
            _clickMovementController.enabled = true;
            _inventoryUi.SetActive(true);
            
            return true;
        }
    }
}
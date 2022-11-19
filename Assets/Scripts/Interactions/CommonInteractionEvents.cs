using Movement;
using UnityEngine;
using Utils.Attributes;

namespace Interactions
{
    public class CommonInteractionEvents : MonoBehaviour
    {
        [SerializeField, AutoProperty(AutoPropertyMode.Scene)]
        private OnClickDestinationSetter _clickMovementController;

        public void OnStartInteraction()
        {
            _clickMovementController.enabled = false;
        }
        
        public void OnEndedInteraction()
        {
            _clickMovementController.enabled = true;
        }
    }
}
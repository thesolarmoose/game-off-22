using System.Threading;
using NaughtyAttributes;
using UnityEngine;

namespace Dialogues.UI
{
    public class DialogueLauncher : MonoBehaviour
    {
        [SerializeField] private Dialogue _dialogue;
        [SerializeField] private DialoguePopup _popupPrefab;

        private CancellationTokenSource _cts;

        private void OnEnable()
        {
            _cts = new CancellationTokenSource();
        }

        private void OnDisable()
        {
            if (!_cts.IsCancellationRequested)
            {
                _cts.Cancel();
            }
            
            _cts.Dispose();
        }

        [Button]
        private async void DisplayDialogue()
        {
            var ct = _cts.Token;
            await AsyncUtils.Popups.ShowPopup(_popupPrefab, _dialogue, ct);
        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using Dialogues.Data;
using Dialogues.View;
using Dialogues.View.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogues.UI
{
    public class CustomDialogueLineRenderer : DialogueLineRendererBase<CustomDialogueLineData>
    {
        [SerializeField] private DialogueText _dialogueTextRenderer;
        [SerializeField] private TextMeshProUGUI _nameText;

        [SerializeField] private Button _interactionButton;
        
        public override async Task RenderLine(CustomDialogueLineData line, CancellationToken ct)
        {
            if (line.Character)
            {
                _nameText.text = line.Character.CharacterName;
                _nameText.color = line.Character.TextColor;
            }
            else
            {
                Debug.LogWarning($"Character is null in dialogue line");
            }
            
            var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(ct);
            var linkedCt = linkedCts.Token;

            try
            {
                var dialogueText = await line.DialogueText.GetLocalizedStringAsync().Task;
                var showTextTask = _dialogueTextRenderer.ShowText(dialogueText, linkedCt);
                var interactionTask = AsyncUtils.Utils.WaitPressButtonAsync(_interactionButton, linkedCt);

                var finishedTask = await Task.WhenAny(showTextTask, interactionTask);
                await finishedTask;  // hack to propagate exceptions

                linkedCts.Cancel();
                
                // ensure entire text is displayed even if async task was cancelled
                _dialogueTextRenderer.ShowAll();
            }
            finally
            {
                linkedCts.Dispose();
            }
            
            await AsyncUtils.Utils.WaitPressButtonAsync(_interactionButton, ct);
        }
    }
}
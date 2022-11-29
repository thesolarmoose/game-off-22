using System.Threading;
using System.Threading.Tasks;
using AsyncUtils;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PauseMenuPopup : AsyncPopup
    {
        [SerializeField] private Button _resumeButton;
        
        public override async Task Show(CancellationToken ct)
        {
            await AsyncUtils.Utils.WaitPressButtonAsync(_resumeButton, ct);
        }

        public override void Initialize()
        {
        }
    }
}
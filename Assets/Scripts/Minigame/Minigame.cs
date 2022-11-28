using System.Threading;
using System.Threading.Tasks;
using AsyncUtils;
using BrunoMikoski.AnimationSequencer;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Minigame
{
    public class Minigame : AsyncPopupReturnable<bool>
    {
        [SerializeField] private Button _headsButton;
        [SerializeField] private Button _tailsButton;
        [SerializeField] private Button _acceptButton;

        [SerializeField] private TextMeshProUGUI _coinResultText;
        [SerializeField] private TextMeshProUGUI _resultText;

        [SerializeField] private string _headsText;
        [SerializeField] private string _tailsText;
        [SerializeField] private string _winText;
        [SerializeField] private string _loseText;

        [SerializeField] private AnimationSequencerController _afterChooseAnimation;

        [SerializeField] private AnimationSequencerController _closeAnimation;

        public override async Task<bool> Show(CancellationToken ct)
        {
            var pressedButton = await AsyncUtils.Utils.WaitFirstButtonPressedAsync(ct, _headsButton, _tailsButton);
            
            var win = Random.value >= 0.5f;

            var correctButton = win && pressedButton == _headsButton ? _headsButton : _tailsButton;
            var coinText = correctButton == _headsButton ? _headsText : _tailsText;
            var resultText = win ? _winText : _loseText;
            
            _coinResultText.text = coinText;
            _resultText.text = resultText;
                
            _afterChooseAnimation.Play();
            await _afterChooseAnimation.PlayingSequence.AsyncWaitForCompletion();

            await AsyncUtils.Utils.WaitPressButtonAsync(_acceptButton, ct);

            _closeAnimation.Play();

            return win;
        }

        public override void Initialize()
        {
            
        }
    }
}
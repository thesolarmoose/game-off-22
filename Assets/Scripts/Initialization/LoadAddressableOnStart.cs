using UnityEngine;
using UnityEngine.Localization;

namespace Initialization
{
    public class LoadAddressableOnStart : MonoBehaviour
    {
        [SerializeField] private LocalizedString _addressableString;

        private void Start()
        {
            StartAsync();
        }

        private async void StartAsync()
        {
            await _addressableString.GetLocalizedStringAsync().Task;
        } 
    }
}
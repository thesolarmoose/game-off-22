using System.Threading;
using UnityEngine;
using UnityEngine.Events;

namespace Items
{
    public class InventoryUiOpener : MonoBehaviour
    {
        [SerializeField] private InventoryUi _inventory;
        [SerializeField] private RectTransform _inventoryRect;
        [SerializeField] private GameObject _openIcon;

        [SerializeField] private UnityEvent _onOpen;
        [SerializeField] private UnityEvent _onClose;

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

        public void Open()
        {
            var ct = _cts.Token;
            OpenAsync(ct);
        }

        private async void OpenAsync(CancellationToken ct)
        {
            _onOpen.Invoke();
            _openIcon.SetActive(false);

            await _inventory.OpenInventory(ct);
            
            _openIcon.SetActive(true);
            _onClose.Invoke();
        }
    }
}

using System.Threading;
using UnityEngine;

namespace Items
{
    public class InventoryUiOpener : MonoBehaviour
    {
        [SerializeField] private InventoryUi _inventory;
        [SerializeField] private RectTransform _inventoryRect;
        [SerializeField] private GameObject _openIcon;

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
            _openIcon.SetActive(false);

            await _inventory.OpenInventory(ct);
            
            _openIcon.SetActive(true);
        }
    }
}

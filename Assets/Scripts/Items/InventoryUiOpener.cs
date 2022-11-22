using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Items
{
    public class InventoryUiOpener : MonoBehaviour
    {
        [SerializeField] private InventoryUi _inventory;

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
            // execute open animation
            _inventory.enabled = true;
            
            // await any of
            //     cursor exit without selection
            //     interacted with item selected
            
            // execute close animation
            _inventory.enabled = false;
        }

        private async Task WaitForCursorLeave()
        {
            
        }
    }
}

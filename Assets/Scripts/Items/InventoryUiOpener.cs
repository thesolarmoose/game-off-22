using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Utils.Input;

namespace Items
{
    public class InventoryUiOpener : MonoBehaviour
    {
        [SerializeField] private InventoryUi _inventory;
        [SerializeField] private RectTransform _inventoryRect;
        [SerializeField] private GameObject _openIcon;

        private InputAction _pointAction;
        private CancellationTokenSource _cts;

        private void Start()
        {
            _pointAction = InputActionUtils.GetPointAction();
            _pointAction.Enable();
        }

        private void OnEnable()
        {
            _cts = new CancellationTokenSource();
            _pointAction?.Enable();
        }

        private void OnDisable()
        {
            if (!_cts.IsCancellationRequested)
            {
                _cts.Cancel();
            }
            
            _cts.Dispose();
            
            _pointAction?.Disable();
        }

        public void Open()
        {
            var ct = _cts.Token;
            OpenAsync(ct);
        }

        private async void OpenAsync(CancellationToken ct)
        {
            // execute open animation
            _openIcon.SetActive(false);
            await _inventory.ExecuteOpenAnimationAsync();

            await WaitForCloseCondition(ct);
            // await any of
            //     cursor exit without selection
            //     interacted with item selected
            
            // execute close animation
            await _inventory.ExecuteCloseAnimationAsync();
            _openIcon.SetActive(true);
        }

        /// <summary>
        /// Close condition: the cursor leaves the inventory and there is no item selected.
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        private async Task WaitForCloseCondition(CancellationToken ct)
        {
            var rayCastResults = new List<RaycastResult>();
            var eventSystem = EventSystem.current;
            var eventData = new PointerEventData(eventSystem);

            bool insideInventory = true;
            bool itemSelected = true;
            while ((insideInventory || itemSelected) && !ct.IsCancellationRequested)
            {
                var screenPosition = _pointAction.ReadValue<Vector2>();
                eventData.position = screenPosition;
                eventSystem.RaycastAll(eventData, rayCastResults);
                insideInventory = rayCastResults.Any(rayCast => rayCast.gameObject == _inventoryRect.gameObject);
                itemSelected = _inventory.ExistsItemSelected();
                await Task.Yield();
            }
        }
    }
}

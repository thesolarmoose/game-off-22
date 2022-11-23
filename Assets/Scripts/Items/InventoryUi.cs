using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BrunoMikoski.AnimationSequencer;
using DG.Tweening;
using ModelView;
using Movement;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Utils.Input;

namespace Items
{
    public class InventoryUi : MonoBehaviour
    {
        [SerializeField] private ItemValueList _inventory;
        [SerializeField] private ViewList _viewList;
        [SerializeField] private RectTransform _inventoryRect;

        [SerializeField] private InteractableEventEvent _onInteractedEvent;

        [SerializeField] private AnimationSequencerController _openAnimation;
        [SerializeField] private AnimationSequencerController _closeAnimation;

        private Action<Item> _onItemSelected;
        private Action<Item> _onItemDeselected;
        
        private InputAction _pointAction;
        private bool _opened;
        private Item _currentSelected;
        private bool _thereIsSelected;
        
        private void Start()
        {
            RegisterInventoryListeners();
            _pointAction = InputActionUtils.GetPointAction();
            _pointAction.Enable();
            _onInteractedEvent.Register(evt => Debug.Log("interacted"));
        }

        private void OnEnable()
        {
            _pointAction?.Enable();
        }
        
        private void OnDisable()
        {
            _pointAction?.Disable();
        }

        private void RegisterInventoryListeners()
        {
            _inventory.Added.Register(OnItemAdded);
            _inventory.Removed.Register(OnItemRemoved);
        }
        
        private void UnregisterInventoryListeners()
        {
            _inventory.Added.Unregister(OnItemAdded);
            _inventory.Removed.Unregister(OnItemRemoved);
        }

        private void OnItemAdded(Item item)
        {
            // list.add
            var view = _viewList.Add(item) as ItemView;
            view.OnSelectEvent.AddListener(() => OnItemSelected(item));
            view.OnDeselectEvent.AddListener(() => OnItemDeselected(item));

            if (_opened)
            {
                // execute add animation
                view.AddAnimation.Play();
            }
        }

        private void OnItemRemoved(Item item)
        {
            // get view
            var exists = _viewList.GetViewFromModel(item, out ItemView view);
            if (exists)
            {
                if (_opened)
                {
                    // execute remove animation
                    view.RemoveAnimation.Play();
                }
                // list.remove
                _viewList.Remove(item);
            }
        }

        private void OnItemSelected(Item item)
        {
            _onItemSelected?.Invoke(item);
        }
        
        private void OnItemDeselected(Item item)
        {
            _onItemDeselected?.Invoke(item);
        }

        private async Task ExecuteOpenAnimationAsync()
        {
            _openAnimation.Play();
            await _openAnimation.PlayingSequence.AsyncWaitForCompletion();
            _opened = true;
        }
        
        private async Task ExecuteCloseAnimationAsync()
        {
            _closeAnimation.Play();
            await _closeAnimation.PlayingSequence.AsyncWaitForCompletion();
            _opened = false;
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
                itemSelected = ExistsItemSelected();
                await Task.Yield();
            }
        }

        private async void EnsureButtonSelection(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                var item = await WaitItemSelectedAction(ct);
                if (item != null)
                {
                    _thereIsSelected = true;
                    _currentSelected = item;
                }
                var deselected = await WaitItemDeselectedAction(ct);
                if (deselected != null)
                {
                    _thereIsSelected = false;
                    _currentSelected = null;
                    Debug.Log("deselected");
                }
            }
        }

        private async Task<Item> WaitItemSelectedAction(CancellationToken ct)
        {
            var item = await WaitItemAction(() => _onItemSelected, (action) => _onItemSelected = action, ct);
            return item;
        }
        
        private async Task<Item> WaitItemDeselectedAction(CancellationToken ct)
        {
            var item = await WaitItemAction(() => _onItemDeselected, (action) => _onItemDeselected = action, ct);
            return item;
        }

        private async Task<Item> WaitItemAction(Func<Action<Item>> getAction, Action<Action<Item>> setAction, CancellationToken ct)
        {
            bool triggered = false;
            Item triggeredItem = null;
            void OnButtonDeselected(Item item)
            {
                triggered = true;
                triggeredItem = item;
            }

            var action = getAction();
            action += OnButtonDeselected;
            setAction.Invoke(action);
            while (!ct.IsCancellationRequested && !triggered)
            {
                await Task.Yield();
            }

            action -= OnButtonDeselected;
            setAction.Invoke(action);

            if (triggered)
            {
                return triggeredItem;
            }

            return null;
        }
        
        public async Task OpenInventory(CancellationToken ct)
        {
            await ExecuteOpenAnimationAsync();

            var selectionCts = CancellationTokenSource.CreateLinkedTokenSource(ct);
            var selectionCt = selectionCts.Token;

            try
            {
                EnsureButtonSelection(selectionCt);
                await WaitForCloseCondition(ct);
                selectionCts.Cancel();
            }
            finally
            {
                selectionCts.Dispose();
            }
            
            await ExecuteCloseAnimationAsync();
        }

        public bool ExistsItemSelected()
        {
            return _thereIsSelected;
        }

        public Item GetSelectedItem()
        {
            return _currentSelected;
        }
    }
}
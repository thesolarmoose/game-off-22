using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BrunoMikoski.AnimationSequencer;
using DG.Tweening;
using FMODUnity;
using ModelView;
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

        [SerializeField] private AnimationSequencerController _openAnimation;
        [SerializeField] private AnimationSequencerController _closeAnimation;
        
        private InputAction _pointAction;
        private bool _isOpened;
        private Item _currentItemSelected;
        private bool _thereIsSelected;

        public bool IsOpened => _isOpened;

        public Item CurrentItemSelected => _currentItemSelected;

        private void Start()
        {
            RegisterInventoryListeners();
            _pointAction = InputActionUtils.GetPointAction();
            _pointAction.Enable();
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

        private void OnItemAdded(Item item)
        {
            // list.add
            var view = _viewList.Add(item) as ItemView;
            view.OnSelectEvent.AddListener(() => OnItemSelected(item));
            view.OnDeselectEvent.AddListener(() => OnItemDeselected(item));
            FMODUnity.RuntimeManager.CreateInstance(item.PickupSound).start();

            if (_isOpened)
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
                if (_isOpened)
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
            _thereIsSelected = true;
            _currentItemSelected = item;
        }
        
        private void OnItemDeselected(Item item)
        {
            _thereIsSelected = false;
            if (IsPointerInsideInventory())
            {
                _currentItemSelected = null;
            }
        }

        private async Task ExecuteOpenAnimationAsync()
        {
            _openAnimation.Play();
            await _openAnimation.PlayingSequence.AsyncWaitForCompletion();
        }
        
        private async Task ExecuteCloseAnimationAsync()
        {
            _closeAnimation.Play();
            await _closeAnimation.PlayingSequence.AsyncWaitForCompletion();
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
                insideInventory = IsPointerInsideInventory(eventData, eventSystem, rayCastResults);
                itemSelected = _thereIsSelected;
                await Task.Yield();
            }
        }

        private bool IsPointerInsideInventory()
        {
            var rayCastResults = new List<RaycastResult>();
            var eventSystem = EventSystem.current;
            var eventData = new PointerEventData(eventSystem);
            return IsPointerInsideInventory(eventData, eventSystem, rayCastResults);
        }
        
        private bool IsPointerInsideInventory(PointerEventData eventData, EventSystem eventSystem, List<RaycastResult> rayCastResults)
        {
            var screenPosition = _pointAction.ReadValue<Vector2>();
            eventData.position = screenPosition;
            eventSystem.RaycastAll(eventData, rayCastResults);
            bool insideInventory = rayCastResults.Any(rayCast => rayCast.gameObject == _inventoryRect.gameObject);
            return insideInventory;
        }
        
        public async Task OpenInventory(CancellationToken ct)
        {
            _currentItemSelected = null;
            _thereIsSelected = false;
            _isOpened = true;
            await ExecuteOpenAnimationAsync();
            
            await WaitForCloseCondition(ct);
            
            await ExecuteCloseAnimationAsync();
            _isOpened = false;
        }
    }
}
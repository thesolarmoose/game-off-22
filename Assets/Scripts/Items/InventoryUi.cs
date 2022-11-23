using System.Threading.Tasks;
using BrunoMikoski.AnimationSequencer;
using DG.Tweening;
using ModelView;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace Items
{
    public class InventoryUi : MonoBehaviour
    {
        [SerializeField] private ItemValueList _inventory;
        [SerializeField] private ViewList _viewList;

        [SerializeField] private AnimationSequencerController _openAnimation;
        [SerializeField] private AnimationSequencerController _closeAnimation;

        private bool _opened;
        private Item _currentSelected;
        private bool _thereIsSelected;
        
        private void Start()
        {
            RegisterInventoryListeners();
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
            _currentSelected = item;
            _thereIsSelected = true;
        }
        
        private void OnItemDeselected(Item item)
        {
            _thereIsSelected = false;
            _currentSelected = null;
        }

        public async Task ExecuteOpenAnimationAsync()
        {
            _openAnimation.Play();
            await _openAnimation.PlayingSequence.AsyncWaitForCompletion();
            _opened = true;
        }
        
        public async Task ExecuteCloseAnimationAsync()
        {
            _closeAnimation.Play();
            await _closeAnimation.PlayingSequence.AsyncWaitForCompletion();
            _opened = false;
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
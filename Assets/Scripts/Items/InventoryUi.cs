using ModelView;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace Items
{
    public class InventoryUi : MonoBehaviour
    {
        [SerializeField] private ItemValueList _inventory;
        [SerializeField] private ViewList _viewList;

        private void Start()
        {
            _inventory.Added.Register(OnItemAdded);
            _inventory.Removed.Register(OnItemRemoved);
        }

        private void OnItemAdded(Item item)
        {
            // list.add
            var view = _viewList.Add(item) as ItemView;
            view.OnSelectEvent.AddListener(() => Debug.Log($"selected {gameObject.name}"));
            view.OnDeselectEvent.AddListener(() => Debug.Log($"deselected {gameObject.name}"));
            
            // execute add animation
        }

        private void OnItemRemoved(Item item)
        {
            // get view
            // execute remove animation
            // list.remove
        }

        public bool ExistsItemSelected()
        {
            return false;
        }

        public Item GetSelectedItem()
        {
            return default;
        }
    }
}
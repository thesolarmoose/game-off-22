using ModelView;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Items
{
    public class ItemView : ViewBaseBehaviour<Item>, ISelectHandler, IDeselectHandler
    {
        [SerializeField] private Button _button;

        [SerializeField] private UnityEvent _onSelectEvent;
        [SerializeField] private UnityEvent _onDeselectEvent;

        public UnityEvent OnSelectEvent => _onSelectEvent;

        public UnityEvent OnDeselectEvent => _onDeselectEvent;

        public override bool CanRenderModel(Item model)
        {
            return true;
        }

        public override void Initialize(Item model)
        {
            
        }

        public override void UpdateView(Item model)
        {
            
        }

        public void OnSelect(BaseEventData eventData)
        {
            _onSelectEvent.Invoke();
        }

        public void OnDeselect(BaseEventData eventData)
        {
            _onDeselectEvent.Invoke();
        }
    }
}
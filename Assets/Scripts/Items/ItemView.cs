using BrunoMikoski.AnimationSequencer;
using ModelView;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Items
{
    public class ItemView : ViewBaseBehaviour<Item>, ISelectHandler, IDeselectHandler
    {
        [SerializeField] private UnityEvent _onSelectEvent;
        [SerializeField] private UnityEvent _onDeselectEvent;

        [SerializeField] private AnimationSequencerController _addAnimation;
        [SerializeField] private AnimationSequencerController _removeAnimation;
        
        public UnityEvent OnSelectEvent => _onSelectEvent;

        public UnityEvent OnDeselectEvent => _onDeselectEvent;

        public AnimationSequencerController AddAnimation => _addAnimation;

        public AnimationSequencerController RemoveAnimation => _removeAnimation;

        public override bool CanRenderModel(Item model)
        {
            return true;
        }

        public override void Initialize(Item model)
        {
            UpdateView(model);
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
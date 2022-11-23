using System;
using BrunoMikoski.AnimationSequencer;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace Items
{
    public class ItemAddedAnimation : MonoBehaviour
    {
        [SerializeField] private ItemEvent _itemAddedEvent;
        [SerializeField] private AnimationSequencerController _animation;

        private void OnEnable()
        {
            _itemAddedEvent.Register(PlayAnimation);
        }
        
        private void OnDisable()
        {
            _itemAddedEvent.Unregister(PlayAnimation);
        }

        private void PlayAnimation()
        {
            _animation.Play();
        }
    }
}
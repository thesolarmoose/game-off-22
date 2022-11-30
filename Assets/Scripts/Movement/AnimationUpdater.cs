using System;
using Pathfinding;
using UnityEngine;

namespace Movement
{
    public class AnimationUpdater : MonoBehaviour
    {
        [SerializeField, NaughtyAttributes.AnimatorParam(nameof(_animator))]
        private int _speedParamHash;
        
        [SerializeField] private Animator _animator;
        [SerializeField] private AIPath _mover;

        private void Update()
        {
            var speed = _mover.velocity.magnitude;
            _animator.SetFloat(_speedParamHash, speed);
        }
    }
}
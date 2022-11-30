using System;
using Pathfinding;
using UnityEngine;

namespace Movement
{
    public class FaceDirectionUpdater : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private AIPath _mover;
        [SerializeField] private bool _invert;

        private void Update()
        {
            var horizontalVelocity = _mover.desiredVelocity.x;
            var sign = Mathf.Sign(horizontalVelocity);
            var invertFactor = _invert ? -1 : 1;
            sign *= invertFactor;

            _target.localScale = new Vector3(sign, 1, 1);
        }
    }
}
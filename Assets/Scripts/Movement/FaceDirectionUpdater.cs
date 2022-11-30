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
            var invertFactor = _invert ? -1 : 1;
            if (horizontalVelocity > 0)
            {
                _target.localScale = new Vector3(invertFactor, 1, 1);
            }
            else if (horizontalVelocity < 0)
            {
                _target.localScale = new Vector3(-invertFactor, 1, 1);
            }
        }
    }
}
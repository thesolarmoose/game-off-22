﻿using UnityEngine;

namespace Movement
{
    public class TargetFollow : MonoBehaviour
    {
        [SerializeField] private Transform _target;

        [SerializeField] private Vector2 _offset;

        [Range(0.0f, 1.0f)]
        [SerializeField] private float _lerpSpeed;

        [SerializeField] private bool _bounded;
        [SerializeField] private Vector2 _xBounds;
        [SerializeField] private Vector2 _yBounds;
        
        [SerializeField] private bool _freezeX;
        [SerializeField] private bool _freezeY;

        private void FixedUpdate()
        {
            MoveTowards();
        }

        private void MoveTowards()
        {
            var targetPosition = _target.position;
            var selfPosition = transform.position + (Vector3) _offset;

            Vector3 newPosition = Vector2.Lerp(selfPosition, targetPosition, _lerpSpeed);
            newPosition.z = selfPosition.z;
            
            if (_bounded)
            {
                newPosition.x = Mathf.Clamp(newPosition.x, _xBounds.x, _xBounds.y);
                newPosition.y = Mathf.Clamp(newPosition.y, _yBounds.x, _yBounds.y);
            }
            
            if (_freezeX)
            {
                newPosition.x = selfPosition.x;
            }
            if (_freezeY)
            {
                newPosition.y = selfPosition.y;
            }
            
            transform.position = newPosition;
        }
    }
}
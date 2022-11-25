﻿using UnityEngine;

 namespace Parallax
{
    public class ParallaxPosition : MonoBehaviour
    {
        [SerializeField] private Transform _camera;
    
        [SerializeField] private float _horizontalSpeed;
        [SerializeField] private float _verticalSpeed;

        private Vector3 _cameraIntersectionPosition;
    
        void Awake () {
	        _cameraIntersectionPosition = transform.position;
        }
        
        void Update ()
        {
	        var center = _cameraIntersectionPosition;
	        var cameraPosition = _camera.position;
	        
	        var offset = center - cameraPosition;
	        var newPosX = center.x + offset.x * _horizontalSpeed;
	        var newPosY = center.y + offset.y * _verticalSpeed;
	        var newPos = new Vector3(newPosX, newPosY, center.z);
	        
	        transform.position = newPos;
        }
    }
}
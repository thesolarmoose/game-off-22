using UnityEngine;

namespace VerticalScaling
{
    [CreateAssetMenu(fileName = "VerticalScalingData", menuName = "VerticalScaling/VerticalScalingData", order = 0)]
    public class VerticalScalingData : ScriptableObject
    {
        [SerializeField] private float _minScale;
        [SerializeField] private float _maxScale;
        [SerializeField] private float _scaleFactor;
        [SerializeField] private float _center;

        public float MinScale => _minScale;

        public float MaxScale => _maxScale;

        public float ScaleFactor => _scaleFactor;

        public float Center => _center;
    }
}
using UnityEngine;

namespace VerticalScaling
{
    [CreateAssetMenu(fileName = "LinearFunctionData", menuName = "VerticalScaling/LinearFunctionData", order = 0)]
    public class LinearFunctionData : ScriptableObject
    {
        [SerializeField] private float _minValue;
        [SerializeField] private float _maxValue;
        [SerializeField] private float _slope;
        [SerializeField] private float _offset;

        public float Evaluate(float x)
        {
            var y = _slope * x + _offset;
            y = Mathf.Clamp(y, _minValue, _maxValue);
            return y;
        }
    }
}
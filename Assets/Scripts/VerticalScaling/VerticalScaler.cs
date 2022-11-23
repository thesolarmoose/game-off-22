using UnityEngine;
using Utils.Attributes;

namespace VerticalScaling
{
    public class VerticalScaler : MonoBehaviour
    {
        [SerializeField, AutoProperty(AutoPropertyMode.Asset)]
        private VerticalScalingData _data;

        private void Update()
        {
            UpdateScale();
        }

        private void UpdateScale()
        {
            var trf = transform;
            var posY = trf.position.y;

            var centerOffset = _data.Center - posY;
            var newScale = _data.ScaleFactor * -posY + _data.Center;
            newScale = Mathf.Clamp(newScale, _data.MinScale, _data.MaxScale);
            
            trf.localScale = Vector3.one * newScale;
        }
    }
}
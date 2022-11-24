using UnityEngine;
using Utils.Attributes;

namespace VerticalScaling
{
    public class VerticalScaler : MonoBehaviour
    {
        [SerializeField, AutoProperty(AutoPropertyMode.Asset)]
        private LinearFunctionData _data;

        private void Update()
        {
            UpdateScale();
        }

        private void UpdateScale()
        {
            var trf = transform;
            var posY = trf.position.y;

            var newScale = _data.Evaluate(-posY);
            
            trf.localScale = Vector3.one * newScale;
        }
    }
}
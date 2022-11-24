using Pathfinding;
using UnityEngine;

namespace VerticalScaling
{
    public class VerticalSpeedScaler : MonoBehaviour
    {
        [SerializeField] private AIPath _path;
        [SerializeField] private LinearFunctionData _functionData;

        private void Update()
        {
            var posYNeg = -transform.position.y;
            var newSpeed = _functionData.Evaluate(posYNeg);
            _path.maxSpeed = newSpeed;
        }
    }
}
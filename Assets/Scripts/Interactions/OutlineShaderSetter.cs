using UnityEngine;

namespace Interactions
{
    public class OutlineShaderSetter : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Material _default;
        [SerializeField] private Material _outline;

        public void SetShader()
        {
            _renderer.material = _outline;
        }

        public void RemoveShader()
        {
            _renderer.material = _default;
        }
    }
}
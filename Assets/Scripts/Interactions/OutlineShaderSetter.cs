using System.Collections.Generic;
using UnityEngine;

namespace Interactions
{
    public class OutlineShaderSetter : MonoBehaviour
    {
        [SerializeField] private List<SpriteRenderer> _renderers;
        [SerializeField] private Material _default;
        [SerializeField] private Material _outline;

        public void SetShader()
        {
            foreach (var spriteRenderer in _renderers)
            {
                spriteRenderer.material = _outline;
            }
        }

        public void RemoveShader()
        {
            foreach (var spriteRenderer in _renderers)
            {
                spriteRenderer.material = _default;
            }
        }
    }
}
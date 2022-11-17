using UnityEngine;

namespace Interactions
{
    public class OutlineShaderSetter : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;

        public void SetShader()
        {
            _renderer.color = Color.black;
        }

        public void RemoveShader()
        {
            _renderer.color = Color.white;
        }
    }
}
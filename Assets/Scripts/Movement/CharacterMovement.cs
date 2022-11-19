using UnityEngine;

namespace Movement
{
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _speed;
    }
}
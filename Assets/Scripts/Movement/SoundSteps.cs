using FMODUnity;
using UnityEngine;

namespace Movement
{
    public class SoundSteps : MonoBehaviour
    {
        [SerializeField] private StudioEventEmitter _emitter;

        public void PlayStepSound()
        {
            _emitter.Play();
        }
    }
}
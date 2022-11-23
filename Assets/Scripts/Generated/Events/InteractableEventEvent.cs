using UnityEngine;
using Interactions;

namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// Event of type `Interactions.InteractableEvent`. Inherits from `AtomEvent&lt;Interactions.InteractableEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/InteractableEvent", fileName = "InteractableEventEvent")]
    public sealed class InteractableEventEvent : AtomEvent<Interactions.InteractableEvent>
    {
    }
}

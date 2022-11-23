using UnityEngine;
using Interactions;

namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// Value List of type `Interactions.InteractableEvent`. Inherits from `AtomValueList&lt;Interactions.InteractableEvent, InteractableEventEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-piglet")]
    [CreateAssetMenu(menuName = "Unity Atoms/Value Lists/InteractableEvent", fileName = "InteractableEventValueList")]
    public sealed class InteractableEventValueList : AtomValueList<Interactions.InteractableEvent, InteractableEventEvent> { }
}

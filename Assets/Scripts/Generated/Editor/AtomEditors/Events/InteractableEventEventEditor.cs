#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;
using UnityAtoms.Editor;
using Interactions;

namespace UnityAtoms.BaseAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `Interactions.InteractableEvent`. Inherits from `AtomEventEditor&lt;Interactions.InteractableEvent, InteractableEventEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(InteractableEventEvent))]
    public sealed class InteractableEventEventEditor : AtomEventEditor<Interactions.InteractableEvent, InteractableEventEvent> { }
}
#endif

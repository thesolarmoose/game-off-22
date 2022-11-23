#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.BaseAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `Interactions.InteractableEvent`. Inherits from `AtomDrawer&lt;InteractableEventEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(InteractableEventEvent))]
    public class InteractableEventEventDrawer : AtomDrawer<InteractableEventEvent> { }
}
#endif

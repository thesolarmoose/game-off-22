#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.BaseAtoms.Editor
{
    /// <summary>
    /// Value List property drawer of type `Interactions.InteractableEvent`. Inherits from `AtomDrawer&lt;InteractableEventValueList&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(InteractableEventValueList))]
    public class InteractableEventValueListDrawer : AtomDrawer<InteractableEventValueList> { }
}
#endif

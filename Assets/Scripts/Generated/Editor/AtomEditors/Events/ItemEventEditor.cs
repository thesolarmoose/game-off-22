#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;
using UnityAtoms.Editor;
using Items;

namespace UnityAtoms.BaseAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `Items.Item`. Inherits from `AtomEventEditor&lt;Items.Item, ItemEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(ItemEvent))]
    public sealed class ItemEventEditor : AtomEventEditor<Items.Item, ItemEvent> { }
}
#endif

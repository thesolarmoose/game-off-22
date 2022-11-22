using UnityEngine;
using Items;

namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// Value List of type `Items.Item`. Inherits from `AtomValueList&lt;Items.Item, ItemEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-piglet")]
    [CreateAssetMenu(menuName = "Unity Atoms/Value Lists/Item", fileName = "ItemValueList")]
    public sealed class ItemValueList : AtomValueList<Items.Item, ItemEvent> { }
}

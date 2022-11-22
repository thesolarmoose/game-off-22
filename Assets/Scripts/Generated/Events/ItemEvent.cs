using UnityEngine;
using Items;

namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// Event of type `Items.Item`. Inherits from `AtomEvent&lt;Items.Item&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/Item", fileName = "ItemEvent")]
    public sealed class ItemEvent : AtomEvent<Items.Item>
    {
    }
}

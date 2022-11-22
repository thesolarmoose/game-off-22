using UnityEngine;
using UnityEngine.Localization;

namespace Items
{
    [CreateAssetMenu(fileName = "Item", menuName = "Items/Item", order = 0)]
    public class Item : ScriptableObject
    {
        [SerializeField] private LocalizedString _itemName;
        [SerializeField] private Sprite _sprite;

        public LocalizedString ItemName => _itemName;
        public Sprite Sprite => _sprite;
    }
}
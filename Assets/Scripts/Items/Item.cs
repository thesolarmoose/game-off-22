using FMODUnity;
using UnityEngine;
using UnityEngine.Localization;

namespace Items
{
    [CreateAssetMenu(fileName = "Item", menuName = "Items/Item", order = 0)]
    public class Item : ScriptableObject
    {
        [SerializeField] private LocalizedString _itemName;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private EventReference _pickupSound;
        [SerializeField] private EventReference _useSound;

        public LocalizedString ItemName => _itemName;
        public Sprite Sprite => _sprite;

        public EventReference PickupSound => _pickupSound;

        public EventReference UseSound => _useSound;
    }
}
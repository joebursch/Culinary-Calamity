using UnityEngine;

namespace Items
{
    public class Item : MonoBehaviour
    {
        // functions like a primary key
        [SerializeField] private int _itemId;
        [SerializeField] private string _itemName;
        [SerializeField] private Sprite _itemSprite;
        [SerializeField] private int _sellPrice;
        [SerializeField] private int _buyPrice;

        /// <summary>
        /// Basic constructor for inventory
        /// </summary>
        /// <param name="name"></param>
        /// <param name="sprite"></param>
        public Item(string name, Sprite sprite)
        {
            _itemName = name;
            _itemSprite = sprite;
        }

        public string GetName() { return _itemName; }
        public Sprite GetSprite() { return _itemSprite; }
    }
}
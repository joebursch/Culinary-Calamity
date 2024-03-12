using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace Items
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private int _itemId; // int for serializable, should be cast to ItemId before use
        [SerializeField] private string _itemName;
        [SerializeField] private Sprite _itemSprite;
        [SerializeField] private int _sellPrice;
        [SerializeField] private int _buyPrice;

        // standard accessors
        public ItemId GetItemId() { return (ItemId)_itemId; }
        public string GetName() { return _itemName; }
        public Sprite GetSprite() { return _itemSprite; }
        public int GetSellPrice() { return _sellPrice; }
        public int GetBuyPrice() { return _buyPrice; }
    }
}
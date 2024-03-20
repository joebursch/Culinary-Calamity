using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    /// <summary>
    /// Provides interface for accessing item id to be used in ItemManager
    /// </summary>
    public enum ItemId : int
    {
        Berry = 0
    }

    /// <summary>
    /// Provides single point of access for items and item children
    /// Allows spawning items, accessing item information (sprite, name, sell price, etc.) using ItemId
    /// Most be a MonoBehaviour in order to instantiate prefabs
    /// Singleton
    /// </summary>
    public class ItemManager : MonoBehaviour
    {
        static ItemManager _itemManager;
        // a working representation - future improvements can be made as long as the interface stays the same
        [SerializeField] private List<GameObject> _itemList; // set in editor using SerializeField

        /// <summary>
        /// Built in awake
        /// </summary>
        private void Awake()
        {
            if (_itemManager == null)
            {
                _itemManager = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// singleton access point
        /// </summary>
        /// <returns>instance</returns>
        public static ItemManager GetItemManager()
        {
            return _itemManager;
        }

        /// <summary>
        /// Spawn an item with the specified id under a parent object
        /// </summary>
        /// <param name="itemId">id of item to spawn</param>
        /// <param name="spawnPos">position to spawn item relative to parent</param>
        /// <param name="spawnRot">rotation to spawn item relative to parent</param>
        /// <param name="parentTransform">parent to nest object under</param>
        /// <returns GameObject>returns spawned game object</returns>
        public GameObject SpawnItemAsChild(ItemId itemId, Vector3 spawnPos, Quaternion spawnRot, Transform parentTransform)
        {
            GameObject item = GetItemPrefab(itemId);
            return Instantiate(item, spawnPos, spawnRot, parentTransform);
        }

        /// <summary>
        /// Spawn an item with the specified id with no parent object
        /// </summary>
        /// <param name="itemId">id of item to spawn</param>
        /// <param name="spawnPos">position to spawn item relative to parent</param>
        /// <param name="spawnRot">rotation to spawn item relative to parent</param>
        /// <returns GameObject>returns spawned game object</returns>
        public GameObject SpawnItem(ItemId itemId, Vector3 spawnPos, Quaternion spawnRot)
        {
            GameObject item = GetItemPrefab(itemId);
            return Instantiate(item, spawnPos, spawnRot);
        }

        /// <summary>
        /// Get GameObject prefab corresponding to itemId
        /// This method will allow us to easily change the representation of storing prefabs without changing any other methods
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public GameObject GetItemPrefab(ItemId itemId)
        {
            return _itemList[(int)itemId];
        }

        /// <summary>
        /// Get name associated with an item id
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public string GetItemName(ItemId itemId)
        {
            return GetItemPrefab(itemId).GetComponent<Item>().GetName();
        }

        /// <summary>
        /// Get sell price associated with an item id
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public int GetItemSellPrice(ItemId itemId)
        {
            return GetItemPrefab(itemId).GetComponent<Item>().GetSellPrice();
        }

        /// <summary>
        /// Get buy price associated with an item id
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public int GetItemBuyPrice(ItemId itemId)
        {
            return GetItemPrefab(itemId).GetComponent<Item>().GetBuyPrice();
        }

        /// <summary>
        /// Get sprite associated with an item id
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public Sprite GetItemSprite(ItemId itemId)
        {
            return GetItemPrefab(itemId).GetComponent<Item>().GetSprite();
        }
    }
}
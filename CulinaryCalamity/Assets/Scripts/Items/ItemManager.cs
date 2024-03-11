using UnityEngine;
using System;
using System.Collections.Generic;

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
        ItemManager _itemManager;
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
        public ItemManager GetItemManager()
        {
            return _itemManager;
        }

        /// <summary>
        /// Spawn an item with the specified id
        /// </summary>
        /// <param name="itemId">id of item to spawn</param>
        /// <param name="spawnPos">position to spawn item relative to parent</param>
        /// <param name="spawnRot">rotation to spawn item relative to parent</param>
        /// <param name="parentTransform">parent to nest object under</param>
        /// <returns GameObject>returns spawned game object</returns>
        public GameObject SpawnItem(ItemId itemId, Vector3 spawnPos, Quaternion spawnRot, Transform parentTransform)
        {
            GameObject item = GetItem(itemId);
            return Instantiate(item, spawnPos, spawnRot, parentTransform) as GameObject;
        }

        public GameObject GetItem(ItemId itemId)
        {
            return _itemList[(int)itemId];
        }
    }
}
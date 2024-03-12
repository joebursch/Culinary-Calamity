using UnityEngine;
using System;
using System.Collections.Generic;
using Items;
using UnityEngine.UI;
using TMPro;

namespace Inventory
{
    /// <summary>
    /// Manages the inventory contents panel of the inventory menu
    /// </summary>
    public class InvContentsPanel : MonoBehaviour
    {
        /// <summary>
        /// used to access various item tile components (sprite, name, quantity fields)
        /// </summary>
        private enum ItemTileChildren : int
        {
            image = 0,
            name = 1,
            amount = 2
        }

        private PlayerInventory _playerInventory;
        [SerializeField] private GameObject _itemTilePrefab;
        private List<GameObject> _itemTiles;
        private float _panelHeight;
        private float _panelWidth;
        private float _tilePrefabHeight;
        private float _tilePrefabWidth;

        /// <summary>
        /// built in awake function, sets panel and tile height/width
        /// </summary>
        private void Awake()
        {
            _panelWidth = gameObject.GetComponent<RectTransform>().rect.width;
            _panelHeight = gameObject.GetComponent<RectTransform>().rect.height;
            _tilePrefabWidth = _itemTilePrefab.GetComponent<RectTransform>().rect.width;
            _tilePrefabHeight = _itemTilePrefab.GetComponent<RectTransform>().rect.height;
        }
        /// <summary>
        /// standard mutator
        /// </summary>
        /// <param name="inv"></param>
        public void SetInventory(PlayerInventory inv)
        {
            _playerInventory = inv;
        }

        /// <summary>
        /// Creates item tiles used in the inventory display and adds them to the _itemTiles attribute
        /// </summary>
        private void BuildItemTiles()
        {
            _itemTiles = new();
            foreach (KeyValuePair<ItemId, int> entry in _playerInventory.InventoryContents)
            {
                // create tile and get references to components
                GameObject itemTile = Instantiate(_itemTilePrefab, gameObject.transform);
                itemTile.SetActive(false);

                GameObject itemSprite = itemTile.transform.GetChild((int)ItemTileChildren.image).gameObject;
                GameObject itemName = itemTile.transform.GetChild((int)ItemTileChildren.name).gameObject;
                GameObject itemAmount = itemTile.transform.GetChild((int)ItemTileChildren.amount).gameObject;

                // temp variables for readability
                ItemId itemId = entry.Key;
                int itemQty = entry.Value;
                ItemManager im = ItemManager.GetItemManager();

                // assign values using ItemManager to get item information
                itemSprite.GetComponent<Image>().sprite = im.GetItemSprite(itemId);
                itemName.GetComponent<TextMeshProUGUI>().text = im.GetItemName(itemId);
                itemAmount.GetComponent<TextMeshProUGUI>().text = "x" + itemQty.ToString();

                _itemTiles.Add(itemTile);
            }
        }

        /// <summary>
        /// Positions the tiles in the correct location on the inventory panel
        /// </summary>
        private void PlaceItemTiles()
        {
            int tilesInRow = (int)(_panelWidth / _tilePrefabWidth);
            int totalTiles = ((int)(_panelHeight / _tilePrefabHeight)) * tilesInRow;

            // tiles are created at 0,0 (center) by default, so we have to shift their position
            float xRange = _panelWidth - _tilePrefabWidth; // since positions are centered, remove half from both end
            float yRange = _panelHeight - _tilePrefabHeight;
            float xPos = -(xRange / 2);
            float yPos = yRange / 2;
            for (int idx = 0; idx < totalTiles && idx < _itemTiles.Count; idx++)
            {
                _itemTiles[idx].GetComponent<RectTransform>().localPosition += new Vector3(xPos, yPos, 0);
                _itemTiles[idx].SetActive(true);
                xPos += _tilePrefabWidth;
                if (idx % tilesInRow == 0)
                {
                    yPos += _tilePrefabHeight;
                }
            }
        }

        /// <summary>
        /// Deletes existing item tiles
        /// </summary>
        private void ResetItemTiles()
        {
            if (_itemTiles == null)
            {
                _itemTiles = new();
                return;
            }
            foreach (GameObject tile in _itemTiles)
            {
                Destroy(tile);
            }
        }

        /// <summary>
        /// Updates display based on current inventory.
        /// </summary>
        public void RefreshDisplay()
        {
            ResetItemTiles(); // delete old tiles
            BuildItemTiles(); // create new tiles
            PlaceItemTiles(); // place new tiles
        }

    }
}
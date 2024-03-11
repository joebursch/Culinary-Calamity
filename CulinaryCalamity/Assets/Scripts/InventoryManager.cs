using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InventoryManager : MonoBehaviour
{
    // contents
    private Inventory _playerInventory;
    // visual components
    [SerializeField] private GameObject _itemTilePrefab;
    [SerializeField] private GameObject _contentsPanel;
    [SerializeField] private GameObject _weaponTile;
    [SerializeField] private GameObject _sprite;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _gold;
    private List<GameObject> _itemTiles;
    // events
    public event EventHandler InventoryClose;

    /// <summary>
    /// Set player name
    /// </summary>
    /// <param name="name">string, player name</param>
    public void SetName(string name)
    {
        _name.text = name;
    }

    /// <summary>
    /// Set player gold amount
    /// </summary>
    /// <param name="gold">int, players current gold</param>
    public void SetGold(int gold)
    {
        _gold.text = "Gold: " + gold.ToString();
    }

    /// <summary>
    /// Set player sprite for this inventory instance
    /// </summary>
    /// <param name="sprite">Sprite, a player's sprite</param>
    public void SetSprite(Sprite sprite)
    {
        _sprite.GetComponent<Image>().sprite = sprite;
    }

    /// <summary>
    /// Set player weapon for this inventory instance
    /// </summary>
    /// <param name="weapon">Weapon, a player's weapon</param>
    public void SetWeapon(Weapon weapon)
    {
        //TODO
    }

    /// <summary>
    /// Set inventory object this menu instance will track
    /// </summary>
    /// <param name="inv">Inventory to track</param>
    public void SetInventory(Inventory inv)
    {
        _playerInventory = inv;
    }

    /// <summary>
    /// activates/deactivates the inventory menu. If the inventory is currently inactive and will be activated
    /// this first updates the visual components (item tiles)
    /// </summary>
    public void ToggleInventory()
    {
        if (!gameObject.activeSelf)
        {
            // update inventory tiles
        }
        gameObject.SetActive(!gameObject.activeSelf);
    }

    /// <summary>
    /// Emits CloseInventory event - prompts subscribers to take necessary actions.
    /// This allows the player class to respond to inventory closes initiated through the UI rather than keyboard/controllers
    /// </summary>
    /// <param name="e">EventArgs.Empty</param>
    protected virtual void OnInventoryClose(EventArgs e)
    {
        InventoryClose?.Invoke(this, e);
    }

    /// <summary>
    /// Linked to 'X' button on inventory menu, calls OnInventoryClose 
    /// </summary>
    public void CloseInventory()
    {
        OnInventoryClose(EventArgs.Empty);
    }
}
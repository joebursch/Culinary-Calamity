using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private Inventory _playerInventory;
    public string PlayerName { get; set; }
    public int PlayerGold { get; set; }
    public Sprite PlayerSprite { get; set; }
    [SerializeField] private GameObject _itemTilePrefab;
    private List<GameObject> _itemTiles;

    public void SetInventory(Inventory pInventory)
    {
        _playerInventory = pInventory;
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        return;
    }
}
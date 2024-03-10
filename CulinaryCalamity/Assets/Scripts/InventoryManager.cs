using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    public void SetName(string name)
    {
        _name.text = name;
    }

    public void SetGold(int gold)
    {
        _gold.text = "Gold: " + gold.ToString();
    }

    public void SetSprite(Sprite sprite)
    {
        _sprite.GetComponent<Image>().sprite = sprite;
    }

    public void SetWeapon(Weapon weapon)
    {
        //TODO
    }

    public void SetInventory(Inventory inv)
    {
        _playerInventory = inv;
    }

    public void ToggleInventory()
    {
        if (!gameObject.activeSelf)
        {
            // update inventory tiles
        }
        gameObject.SetActive(!gameObject.activeSelf);
    }


}
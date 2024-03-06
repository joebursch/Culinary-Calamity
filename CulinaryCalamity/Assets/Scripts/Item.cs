using UnityEngine;


public class Item : MonoBehaviour
{
    // functions like a primary key
    [SerializeField] private int _itemId;
    [SerializeField] private string _itemName;
    [SerializeField] private Sprite _itemSprite;
    [SerializeField] private int _sellPrice;
    [SerializeField] private int _buyPrice;
    public string GetName() { return _itemName; }

    [SerializeField] private LayerMask _interactableObjectsLayer;



    void Update()
    {

    }
}
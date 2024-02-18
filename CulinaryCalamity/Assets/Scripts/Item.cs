using UnityEngine;


public class Item : MonoBehaviour
{
    // functions like a primary key
    private int itemId;
    [SerializeField] private Sprite itemSprite;
    private int sellPrice;
    private int buyPrice;
}
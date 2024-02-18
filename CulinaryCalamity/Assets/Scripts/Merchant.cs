using System.Collections.Generic;
using UnityEngine;


public class Merchant : Character
{
    private List<string> saleInventory;
    private int amountGold;

    public Merchant(string name, Sprite sprite, float speed, List<Animation> animations, List<string> inventory, int amountGold) 
    : base(name, sprite, speed, animations)
    { 
        this.saleInventory = inventory;
        this.amountGold = amountGold;
    }

}
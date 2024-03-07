using System.Collections.Generic;
using UnityEngine;


public class Merchant : Character, InteractableObject
{
    private List<string> saleInventory;
    private int amountGold;
    private readonly Dictionary<string, string> dialogue;


}
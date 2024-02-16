using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFile : MonoBehaviour
{
   void Awake()
   {
        Character c = new Character("Bob", 2f, 10);
        Debug.Log(c.getName());

        List<string> inventory = new List<string>(){"rock", "book"};
        Player p = new Player("John", 2f, 13, inventory, 150);
        Debug.Log(p.getName());
   }
}


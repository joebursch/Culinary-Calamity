using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // Private instance variables
    private string characterName; 
    private Sprite mainSprite;
    private float movementSpeed;
    private int characterHealth; 

    // Public instance variables
    public List<Animation> characterAnimations; 

    // Constructors
    public Character(string name, float speed, int health)
    {
        characterName = name;
        movementSpeed = speed;
        characterHealth = health;
    }
    public Character(string name, Sprite sprite, float speed, int health, List<Animation> animations)
    {
        characterName = name;
        mainSprite = sprite;
        movementSpeed = speed;
        characterHealth = health;
        characterAnimations = animations;
    }
}

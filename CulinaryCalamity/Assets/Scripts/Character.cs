using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // Private instance variables

    [SerializeField]
    private string characterName;
    
    [SerializeField]
    private Sprite mainSprite;
    [SerializeField]
    protected float movementSpeed;
    [SerializeField]
    protected int characterHealth;
    [SerializeField]
    private List<Animation> characterAnimations;
    [SerializeField]
    private Animator characterAnimator;

    public Character()
    {

    }
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

    public string getName() { return characterName; }
}




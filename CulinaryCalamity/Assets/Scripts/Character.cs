using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // Private instance variables
    [SerializeField] protected string characterName;
    [SerializeField] protected float movementSpeed;
    [SerializeField] protected int characterHealth;
    protected Animator characterAnimator;
    protected int currentHealth;
}




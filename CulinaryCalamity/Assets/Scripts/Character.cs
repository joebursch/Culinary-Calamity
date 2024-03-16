using UnityEngine;

public class Character : MonoBehaviour
{
    // Private instance variables
    [SerializeField] protected string characterName;
    protected int movementSpeed;
    [SerializeField] protected int characterHealth;
    protected Animator characterAnimator;
    protected int currentHealth;
}




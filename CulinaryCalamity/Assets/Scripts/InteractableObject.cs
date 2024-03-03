using UnityEngine;
using UnityEngine.InputSystem;


public abstract class InteractableObject : MonoBehaviour
{
    protected bool playerCanPickUp;
    protected bool playerCanUse;
    protected int objectHealth;

    public abstract void PickUp();

    public abstract void Use();

    public abstract void Hurt();
}
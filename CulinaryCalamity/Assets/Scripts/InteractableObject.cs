using UnityEngine;
using UnityEngine.InputSystem;


public class InteractableObject : MonoBehaviour
{
    // what key the object responds to (abstracted as InputAction)
    [SerializeField] private InputAction interactionKey;
}
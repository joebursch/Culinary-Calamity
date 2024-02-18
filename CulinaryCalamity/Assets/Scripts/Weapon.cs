using UnityEngine;


public class Weapon : Item
{
    // allows us to assign and update animations
    [SerializeField] private Animator weaponAnimator;
    private int weaponDmg;
}
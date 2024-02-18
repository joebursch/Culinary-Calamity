using System.Collections.Generic;
using UnityEngine;


public class Creature : Character
{
    private int currentHealth;
    private int damage;

    private Item dropItem;

    public Animation attackAnimation;

    public Animation deathAnimation;


    public Creature(string name, Sprite sprite, float speed, int health, List<Animation> animations, int currentHealth, int damage, Item dropItem, Animation attack, Animation death)
        : base(name, sprite, speed, health, animations)
    {
        this.currentHealth = currentHealth;
        this.damage = damage;
        this.dropItem = dropItem;
        this.attackAnimation = attack;
        this.deathAnimation = death;
    }


}

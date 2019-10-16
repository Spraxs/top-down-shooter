using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
    public new string name;

    public int level;

    public float baseHealth;
    public float baseDamage;

    public float baseMovementSpeed;

    public float targetRange;

    public float attackCooldown;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player", menuName = "Player")]
public class Player : ScriptableObject
{
    public new string name;

    public long id;

    public float health;
    public float maxHealth;

    public float movementSpeed;
}

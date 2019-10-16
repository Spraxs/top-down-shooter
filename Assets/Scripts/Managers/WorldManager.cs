using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public static Action<GameObject, GameObject, float, Vector3> DAMAGE_BY_BULLET;

    public static Action<GameObject, GameObject, float> DAMAGE_BY_ENEMY;


    void Start()
    {
        DAMAGE_BY_BULLET += OnDamageByBullet;
        DAMAGE_BY_ENEMY += OnPlayerDamageByEnemy;
    }

    public void OnDamageByBullet(GameObject damaged, GameObject damager, float damage, Vector3 direction)
    {
        Zombie zombie = damaged.GetComponent<Zombie>();

        if (zombie != null)
        {
            zombie.Damage(damage);

            direction *= 1000f;

            zombie.GetComponent<Rigidbody2D>().AddForce(direction, ForceMode2D.Force);

            return;
        }
    }

    public void OnPlayerDamageByEnemy(GameObject damaged, GameObject damager, float damage)
    {
        PlayerHandler playerHandler = damaged.GetComponent<PlayerHandler>();

        if (playerHandler != null)
        {
            playerHandler.Damage(damage);

            Vector3 heading = damaged.transform.position - damager.transform.position;
            float distance = heading.magnitude;
            Vector3 direction = heading / distance; // This is now the normalized direction.

            direction *= 100f;

            playerHandler.GetComponent<Rigidbody2D>().AddForce(direction, ForceMode2D.Impulse);

            return;
        }
    }
}

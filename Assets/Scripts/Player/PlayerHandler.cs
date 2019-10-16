using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHandler : MonoBehaviour
{
    public Player player;

    public void Damage(float damage)
    {
        player.health -= damage;

        if (player.health <= 0)
        {
            player.health = 0;

            Destroy(gameObject);
        }
    }
}

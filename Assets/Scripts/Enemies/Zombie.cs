using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
using UnityEngine.UI;

public class Zombie : MonoBehaviour
{

    [SerializeField] private Enemy enemy;

    [SerializeField] private Text levelTest;

    [SerializeField] private Slider healthSlider;

    private float maxHealth;

    private float health;

    private float damage;

    private float movementSpeed;

    private bool canAttack = true;

    private GameObject target;

    private PlayerManager playerManager;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = PlayerManager.instance;
        health = enemy.baseHealth + (enemy.baseHealth / 10 * enemy.level);
        maxHealth = health;
        damage = enemy.baseDamage + (enemy.baseDamage / 10 * enemy.level);
        movementSpeed = enemy.baseMovementSpeed + (enemy.baseMovementSpeed / 12 * enemy.level);

        levelTest.text = enemy.level.ToString();
    }

    void Update()
    {
        float distance = -1f;

        foreach (Player player in playerManager.GetAllPlayers())
        {
            GameObject gameObject = playerManager.GetGameObjectById(player.id);

            float dist = Vector2.Distance(transform.position, gameObject.transform.position);

            if (distance == -1f || dist < distance)
            {
                distance = dist;

                target = gameObject;
            }
        }
    }

    void FixedUpdate()
    {
        if (target == null) return;

        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, movementSpeed * Time.deltaTime);
    }


    void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.collider.tag != "Player") return;

        DamageIfInRange(collision.collider.gameObject);
    }

    private void DamageIfInRange(GameObject target)
    {
        if (target == null || !canAttack) return;

        WorldManager.DAMAGE_BY_ENEMY?.Invoke(target, gameObject, damage);

        StartCoroutine(AttackCooldown());
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false;

        yield return new WaitForSeconds(enemy.attackCooldown);

        canAttack = true;
    }


    public void Damage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            health = 0;

            Destroy(gameObject);

            return;
        }

        healthSlider.value = health / maxHealth;
    }
}

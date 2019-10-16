using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    
    private Vector2 beginPos;
    private Vector2 targetPos;
    private GameObject shooter;

    private float speed;
    [SerializeField] private float maxRange;

    [SerializeField] private float hitRange;

    private ObjectPool objectPool;

    private float damage;

    private bool active = false;

    void Start()
    {
        objectPool = FindObjectOfType<ObjectPool>();
    }

    void OnEnable()
    {
        if (!active) gameObject.SetActive(false);
    }

    public void Shoot(Vector2 beginPos, Vector2 endPos, RaycastHit2D raycastHit, GameObject shooter, float projectileSpeed, float damage)
    {
        this.speed = projectileSpeed;
        this.shooter = shooter;
        this.beginPos = beginPos;
        targetPos = endPos;
        this.damage = damage;

        transform.position = beginPos;

        FixRotation(endPos);

        active = true;
        gameObject.SetActive(true);
    }

    private void FixRotation(Vector2 targetPos)
    {
        Vector3 lookPos = targetPos;
        lookPos -= transform.position;
        float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
        angle += 0f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void Update()
    {
        if (active)
        {
            Vector3 heading = targetPos - (Vector2) transform.position;
            float distance = heading.magnitude;
            Vector3 direction = heading / distance; // This is now the normalized direction.

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, hitRange);

            // If it hits something...
            if (hit.collider != null && hit.collider.gameObject != shooter)
            {

                BulletHit(hit);
                return;
            }
        }
    }
        
    

    void FixedUpdate()
    {
        if (active)
        {

            if (Vector2.Distance(gameObject.transform.position, beginPos) > maxRange)
            {
                ResetProjectile();
                return;
            }

            Vector3 heading = targetPos - beginPos;
            float distance = heading.magnitude;
            Vector3 direction = heading / distance; // This is now the normalized direction.

            transform.position += direction * Time.deltaTime * speed;
        }
    }

    private void BulletHit(RaycastHit2D raycastHit)
    {
        GameObject hitObject = raycastHit.collider.gameObject;

        Vector3 heading = targetPos - beginPos;
        float distance = heading.magnitude;
        Vector3 direction = heading / distance; // This is now the normalized direction.

        WorldManager.DAMAGE_BY_BULLET?.Invoke(hitObject, shooter, damage, direction); // Activates when DAMAGE_BY_GAMEOBJECT is not null

        ResetProjectile();
    }

    private void ResetProjectile()
    {
        active = false;
        objectPool.PoolObject(gameObject);
    }
}

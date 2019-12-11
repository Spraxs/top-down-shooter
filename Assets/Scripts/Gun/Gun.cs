using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Input fields

    [SerializeField] protected float damage;

    [SerializeField]
    protected float range;

    [SerializeField] protected float accuracyShotDecrease;

    [SerializeField] protected float fireRate; // Bullets / sec
    [SerializeField] protected FireType fireType;

    [SerializeField] protected GameObject muzzleGameObject;

    [SerializeField] protected AudioClip gunShotClip;

    [SerializeField] protected Corsair corsair;

    protected AudioSource audioSource;


    [Header("Bullet Line")]
    [SerializeField]
    private LineRenderer lineRenderer;


    /*
     * Vars for full auto 
     */
    private bool triggerPulled = false;
    private bool waitForShot;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        muzzleGameObject.SetActive(false);
        InputManager.inputAction += HandleTrigger;
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerPulled && !waitForShot)
        {

            SpawnBullet();
        }
    }

    void OnDisable()
    {
        StopAllCoroutines();
        waitForShot = false;
        muzzleGameObject.SetActive(false);

    }

    private void SpawnBullet()
    {
        if (audioSource == null) return; // User is dead

        audioSource.PlayOneShot(gunShotClip);
        StartCoroutine(FireCooldown());
        StartCoroutine(Flash());

        StartCoroutine(InitBullet());
    }

    private IEnumerator InitBullet()
    {
        Vector2 beginPos = new Vector2(corsair.transform.position.x, corsair.transform.position.y);

        Vector2 direction = corsair.RandomBulletDirection();

        direction.Normalize();

        int layerMask = 1 << 8; //layer 8 is Player Layer
        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8.
        //The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        RaycastHit2D raycastHit = Physics2D.Raycast(beginPos, direction, range, layerMask);

        corsair.EffectCorsair(accuracyShotDecrease);

        lineRenderer.SetPosition(0, beginPos);
        lineRenderer.SetPosition(1, raycastHit.point);
        HitCollider(raycastHit);

        WebManager.Instance.SendPacket(new PacketOutPlayerShootRay(beginPos, direction));

        lineRenderer.enabled = true;

        yield return new WaitForSeconds(0.05f);

        lineRenderer.enabled = false;
    }

    private void HitCollider(RaycastHit2D raycastHit)
    {
        EffectManager.Instance.Play(EffectManager.EffectType.EXPLOSION, raycastHit.point);
    }

    private IEnumerator FireCooldown()
    {
        waitForShot = true;
        yield return new WaitForSeconds(1f / fireRate);
        waitForShot = false;
    }

    private IEnumerator Flash()
    {
        muzzleGameObject.SetActive(true);
        yield return new WaitForSeconds(.01f);
        muzzleGameObject.SetActive(false);
    }

    public void HandleTrigger(InputManager.InputType inputType, float value)
    {
        if (!gameObject.activeSelf) return;

        if (inputType == InputManager.InputType.FIRE)
        {

            if (fireType == FireType.FULL)
            {
                triggerPulled = value > 0;
            }
            else if (fireType == FireType.SEMI && value > 0 && !waitForShot)
            {
                SpawnBullet();
            }

        } else if (inputType == InputManager.InputType.FIRE_2)
        {

            corsair.aiming = value > 0;
        }
    }

    public enum FireType
    {
        FULL, BURST, SEMI
    }
}

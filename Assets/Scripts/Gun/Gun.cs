﻿using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Win32.SafeHandles;
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

    [SerializeField] protected Client client;

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
        lineRenderer.enabled = false; // Probably fixed bug [1.1]
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

        Vector2 endPosition;

        bool isHit = raycastHit.collider != null;

        if (!isHit)
        {
            endPosition = beginPos + direction * range;
        }
        else
        {
            endPosition = raycastHit.point;

            HitCollider(endPosition);
        }


        lineRenderer.SetPosition(1, endPosition);

        WebManager.Instance.SendPacket(new PacketOutPlayerShootRay(beginPos, direction, endPosition, isHit));

        lineRenderer.enabled = true;

        yield return new WaitForSeconds(0.05f);

        lineRenderer.enabled = false;
    }

    private void HitCollider(Vector2 position)
    {
        EffectManager.Instance.Play(EffectManager.EffectType.EXPLOSION, position);
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

        if (client == null)
        {
            Debug.LogWarning("Client has not been set! Check the inspector.");
            return;
        }

        if (!client.isActiveAndEnabled) return;

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

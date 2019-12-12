using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour
{
    public long id;

    public string accountName = "Guest";

    public float health;

    public float maxHealth;

    private bool canUpdatePos = true;

    [SerializeField] private bool isPlayableClient = false;

    [Header("Bullet")] [SerializeField] private LineRenderer bulletLine;

    void OnEnable()
    {
        canUpdatePos = true;
    }

    void OnDisable()
    {
        canUpdatePos = false;

        if (!isPlayableClient) // Probably fixed bug [1.1]
        {
            bulletLine.enabled = false;
        }
    }

    void Update()
    {
        UpdatePosition();
    }

    void UpdatePosition()
    {
        if (!canUpdatePos) return;
        if (!isPlayableClient) return;

        Vector3 pos = transform.position;

        ClientManager.Instance.SendClientPosition(pos.x, pos.y);

        StartCoroutine(PositionUpdateCooldown());

    }

    IEnumerator PositionUpdateCooldown()
    {
        canUpdatePos = false;
        yield return new WaitForSeconds(0.05f);
        canUpdatePos = true;
    }

    public void PlayShootEffect(Vector2 beginPos, Vector2 hitPos, bool isHit)
    {
        if (isPlayableClient) return;
        if (!isActiveAndEnabled) return;

        bulletLine.SetPosition(0, beginPos);
        bulletLine.SetPosition(1, hitPos);

        if (isHit)
        {
            HitCollider(hitPos);
        }

        StartCoroutine(ViewBullet());
    }

    private void HitCollider(Vector2 position)
    {
        EffectManager.Instance.Play(EffectManager.EffectType.EXPLOSION, position);
    }

    IEnumerator ViewBullet()
    {
        bulletLine.enabled = true;

        yield return new WaitForSeconds(0.05f);

        bulletLine.enabled = false;
    }
}

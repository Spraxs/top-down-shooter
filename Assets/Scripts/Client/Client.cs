using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour
{
    public long id;

    public string accountName;

    public float health;

    public float maxHealth;

    private bool canUpdatePos = true;

    void Update()
    {
        UpdatePosition();
    }

    void UpdatePosition()
    {
        if (!canUpdatePos) return;

        Vector3 pos = transform.position;

        ClientManager.Instance.SendClientPosition(pos.x, pos.y);

        StartCoroutine(PositionUpdateCooldown());

    }

    IEnumerator PositionUpdateCooldown()
    {
        canUpdatePos = false;
        yield return new WaitForSeconds(0.2f);
        canUpdatePos = true;
    }
}

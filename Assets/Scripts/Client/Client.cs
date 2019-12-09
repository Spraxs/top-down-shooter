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
}

using System.Collections;
using UnityEngine;
using TMPro;


public class Client : MonoBehaviour
{
    public long id;

    public string accountName = "Guest";

    public float health;

    public float maxHealth;

    private bool canUpdatePos = true;

    private Team team = Team.DEFAULT;

    public float handPositionX;
    public float handPositionY;
    
    public float handRotationZ;
    public float handScaleX;

    public Team Team {

        set
        {
            team = value;

            if (team == Team.RED)
            {
                nameText.color = Color.red;
            } else if (team == Team.BLUE)
            {
                nameText.color = Color.blue;
            } else
            {
                nameText.color = Color.white;
            }

        }

        get
        {
            return team;
        }
    }

    [SerializeField] private bool isPlayableClient = false;

    [Header("Bullet")] [SerializeField] private LineRenderer bulletLine;

    [Header("Name")] [SerializeField] private TMP_Text nameText;

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


        ClientManager.Instance.SendClientHandTransform(handPositionX, handPositionY,
            handRotationZ, handScaleX);

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMovement : MonoBehaviour
{

    [SerializeField] private Client client;
    
    [SerializeField] private float offset;

    [SerializeField] private Transform leftHandHolder;
    [SerializeField] private Transform rightHandHolder;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = transform.root.gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        MouseL();
    }



    void MouseL()
    {
        //Gets mouse position, you can define Z to be in the position you want the weapon to be in
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        Vector3 lookPos = Camera.main.ScreenToWorldPoint(mousePos);

        if (lookPos.x < transform.root.position.x)
        {
            if (!spriteRenderer.flipX)
            {
                offset = 180;
                spriteRenderer.flipX = true;
                transform.position = leftHandHolder.position;
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }

        } else if (spriteRenderer.flipX)
        {
            offset = 0;
            spriteRenderer.flipX = false;
            transform.position = rightHandHolder.position;
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        lookPos = lookPos - transform.position;
        float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
        angle += offset;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        client.handPositionX = transform.position.x;
        client.handPositionY = transform.position.y;
        client.handRotationZ = transform.rotation.eulerAngles.z;
        client.handScaleX = transform.localScale.x;
    }
}

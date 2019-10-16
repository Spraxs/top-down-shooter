using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class Corsair : MonoBehaviour
{
    [Header("Corsair Lines")]
    public Transform leftLine;
    public Transform rightLine;

    [Header("Corsair Offsets")]
    [Range(0, 90)]
    public float maxOffset;

    [Range(0, 90)]
    public float minOffset;

    [Range(0, 100f)]
    public float offsetPercentage;

    [Header("Corsair Rotation")]
    public float rotationOffset;


    [Header("Aiming")]
    public float aimSpeed;


    [HideInInspector]
    public bool aiming;

    void FixedUpdate()
    {
        if (aiming)
        {
            offsetPercentage = Mathf.Lerp(offsetPercentage, 0, aimSpeed * Time.deltaTime);
        }
        else
        {
            offsetPercentage = Mathf.Lerp(offsetPercentage, 100, aimSpeed * Time.deltaTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        MouseL();

        UpdateLinesOffset(offsetPercentage);
    }



    void MouseL()
    {
        //Gets mouse position, you can define Z to be in the position you want the weapon to be in
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        Vector3 lookPos = Camera.main.ScreenToWorldPoint(mousePos);

        lookPos = lookPos - transform.position;
        float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
        angle += rotationOffset;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void UpdateLinesOffset(float percentage)
    {
        float leftLineOffset = CalculateOffset(percentage, false);
        leftLine.localRotation = Quaternion.Euler(0, 0, leftLineOffset);


        float rightLineOffset = CalculateOffset(percentage, true);

        rightLine.localRotation = Quaternion.Euler(0, 0, rightLineOffset);
    }

    public Vector2 RandomBulletDirection()
    {
        float x = Random.Range(leftLine.up.x, rightLine.up.x);
        float y = Random.Range(leftLine.up.y, rightLine.up.y);

        return new Vector2(x, y);
    }

    public void EffectCorsair(float accuracyDecrease)
    {
        offsetPercentage += accuracyDecrease;
    }

    float CalculateOffset(float percentage, bool rightLine)
    {
        float offset = maxOffset / 100 * percentage;

        if (offset < minOffset) offset = minOffset;

        if (rightLine) offset *= -1;

        return offset;
    }
}

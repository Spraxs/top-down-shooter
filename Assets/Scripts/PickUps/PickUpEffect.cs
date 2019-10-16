using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpEffect : MonoBehaviour
{
    private Light light;

    [SerializeField] float speed;
    [SerializeField] private float maxIntensity;
    private bool glow;

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (light == null)
        {
            Debug.Log("Removed PickUpEffect, because no light on object!");
            Destroy(this);
        }

        float newIntensity = light.intensity;


        if (!glow)
        {
            newIntensity -= speed * Time.deltaTime;

            if (newIntensity <= 0)
            {
                newIntensity = 0;

                glow = true;
            }
        }
        else
        {
            newIntensity += speed * Time.deltaTime;

            if (newIntensity >= maxIntensity)
            {
                newIntensity = maxIntensity;

                glow = false;
            }
        }

        light.intensity = newIntensity;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider slider;

    private PlayerHandler playerHandler;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        playerHandler = FindObjectOfType<PlayerHandler>();

        if (playerHandler != null)
            slider.value = playerHandler.player.health / playerHandler.player.maxHealth;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerHandler == null) playerHandler = FindObjectOfType<PlayerHandler>();

        float value = 0;
        if (playerHandler != null)
        {

            value = playerHandler.player.health / playerHandler.player.maxHealth;
        }

        if (slider.value != value)
            slider.value = Mathf.Lerp(slider.value, value, .85f);
    }
}

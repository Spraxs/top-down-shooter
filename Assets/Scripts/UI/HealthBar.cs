using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private ClientManager clientManager;

    private Slider slider;

    private Client client;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        clientManager = ClientManager.Instance;

        if (clientManager == null) return;

        if (client != null)
            slider.value = client.health / client.maxHealth;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (clientManager == null)
        {
            clientManager = ClientManager.Instance;
            return;
        }

        if (client == null) client = ClientManager.Instance.currentClient;

        float value = 0;
        if (client != null)
        {

            value = client.health / client.maxHealth;
        }

        if (slider.value != value)
            slider.value = Mathf.Lerp(slider.value, value, .85f);
    }
}

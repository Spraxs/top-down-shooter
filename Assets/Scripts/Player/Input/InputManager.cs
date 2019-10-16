using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    public static Action<InputType, float> inputAction;

    private Dictionary<InputType, float> lastInput = new Dictionary<InputType, float>();

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (inputAction == null) return;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        UpdateInput(InputType.HORIZONTAL, horizontal);
        UpdateInput(InputType.VERTICAL, vertical);

        bool fire = Input.GetButtonDown("Fire");

        if (fire)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                fire = false;
            }
        }

        UpdateInput(InputType.FIRE, fire ? 1 : 0);

        bool fire2 = Input.GetButton("Fire2");

        if (fire)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                fire2 = false;
            }
        }

        UpdateInput(InputType.FIRE_2, fire2 ? 1 : 0);
    }

    private void UpdateInput(InputType inputType, float value)
    {
        if (lastInput.ContainsKey(inputType))
         {
             if (lastInput[inputType] != value)
             {
                 // Update when values are changed
                 inputAction(inputType, value);
                 lastInput[inputType] = value;

             }

             return;
         }

         lastInput.Add(inputType, value);
        inputAction(inputType, value);
    }


    public enum InputType
    {
        HORIZONTAL, VERTICAL, FIRE, FIRE_2, HOTBAR_1, HOTBAR_2, HOTBAR_3, HOTBAR_4, HOTBAR_5
    }
}

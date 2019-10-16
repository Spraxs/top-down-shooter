using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float vertical;

    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {
        InputManager.inputAction += HandleMovement;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (horizontal != 0)
        {
            MoveHorizontal();
        }

        if (vertical != 0)
        {
            MoveVertical();
        }
    }

    private void MoveHorizontal()
    {
        Vector3 position = transform.position;
        transform.position = new Vector3(position.x + (horizontal * speed * Time.deltaTime), position.y, position.z);
    }

    private void MoveVertical()
    {
        Vector3 position = transform.position;
        transform.position = new Vector3(position.x, position.y + (vertical * speed * Time.deltaTime), position.z);
    }


    public void HandleMovement(InputManager.InputType inputType, float value)
    {
        if (inputType == InputManager.InputType.HORIZONTAL)
        {
            horizontal = value;
        } else

        if (inputType == InputManager.InputType.VERTICAL)
        {
            vertical = value;
        }
    }
}

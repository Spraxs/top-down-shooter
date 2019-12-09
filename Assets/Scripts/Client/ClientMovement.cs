using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class ClientMovement : MonoBehaviour
{

    private readonly static float MAX_DISTANCE = 30.0f;

    private Vector2 currentPosition;

    void FixedUpdate()
    {
        if (!currentPosition.Equals(transform.position))
        {
            LerpMovement(currentPosition);
        }
    }

    public void HandleClientMovement(Vector2 position)
    {
        currentPosition = position;
    }

    private void LerpMovement(Vector2 position)
    {
        Vector2 oldPosition = gameObject.transform.position;

        if (Vector2.Distance(position, oldPosition) > MAX_DISTANCE)
        {
            gameObject.transform.position = position;
        }
        else
        {
            gameObject.transform.position = Vector2.Lerp(oldPosition, position, Time.fixedDeltaTime * 10f);
        }
    }
}

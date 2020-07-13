using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class ClientMovement : MonoBehaviour
{

    private readonly static float MAX_DISTANCE = 30.0f;

    [SerializeField] private Transform handTransform;
    
    private Vector2 currentPosition;

    private Vector2 currentHandPosition;
    private float currentHandRotationZ;
    private float currentHandScaleX;

    void FixedUpdate()
    {
        if (!currentPosition.Equals(transform.position))
        {
            LerpMovement(currentPosition);
        }
        
        LerpHandMovement();
    }

    public void HandleHandMovement(Vector2 handPosition, float handRotationZ, float handScaleX)
    {
        currentHandPosition = handPosition;
        currentHandRotationZ = handRotationZ;
        currentHandScaleX = handScaleX;
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
    
    private void LerpHandMovement()
    {
        // Position
        Vector2 oldPosition = handTransform.position;

        if (Vector2.Distance(currentHandPosition, oldPosition) > MAX_DISTANCE)
        {
            handTransform.position = currentHandPosition;
        }
        else
        {
            handTransform.position = Vector2.Lerp(oldPosition, currentHandPosition, Time.fixedDeltaTime * 10f);
        }
        
        // Rotation
        var oldRotation = handTransform.rotation;

        oldRotation.eulerAngles = new Vector3(oldRotation.eulerAngles.x, oldRotation.eulerAngles.y, currentHandRotationZ);
        
        handTransform.rotation = oldRotation;

        // Scale
        handTransform.localScale = new Vector3(currentHandScaleX, 1, 1);
    }
}

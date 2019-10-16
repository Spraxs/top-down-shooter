using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    private PlayerManager playerManager;
    [SerializeField] private GameObject playerObject;

    [SerializeField] private float speed;

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector3 newPos = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, newPos, speed * Time.deltaTime);
    }
}

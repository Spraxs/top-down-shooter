using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    private GameObject playerObject;

    [SerializeField] private float speed;

    private bool wait = false;

    private IEnumerator FindPlayer()
    {
        wait = true;
        yield return new WaitForSeconds(5);

        wait = false;

        playerObject = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (playerObject == null)
        {
            if (!wait)
            {
                StartCoroutine(FindPlayer());
            }

            if (playerObject == null) return;
        }

        Vector3 newPos = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, newPos, speed * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTest : MonoBehaviour
{
    private Collider2D collider;

    [SerializeField]
    private GameObject test1;

    [SerializeField]
    private GameObject test2;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    void Update()
    {
        Vector2 min = collider.bounds.min;
        Vector2 max = collider.bounds.max;

        Debug.Log("min: " + min);
        Debug.Log("max: " + max);

        test1.transform.position = min;
        test2.transform.position = max;
    }
}

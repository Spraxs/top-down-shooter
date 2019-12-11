using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDebug : MonoBehaviour
{
    [SerializeField] private GameObject test1;

    [SerializeField] private GameObject test2;

    [SerializeField] private GameObject test3;

    [SerializeField] private GameObject test4;

    public static ColliderDebug Instance;

    void OnEnable()
    {
        Instance = this;
    }

    public void UpdateDebugDots(Vector2 pointA, Vector2 pointB, Vector2 pointC, Vector2 pointD)
    {
        if (test1 == null || test2 == null || test3 == null || test4 == null)
        {
            Debug.LogWarning("Not test object has been set! Check the inspector.");
            return;
        }

        test1.transform.position = pointA;

        test2.transform.position = pointB;

        test3.transform.position = pointC;

        test4.transform.position = pointD;
    }


}

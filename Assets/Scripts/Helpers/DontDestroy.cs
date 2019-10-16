using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    // Awake is called before start
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}

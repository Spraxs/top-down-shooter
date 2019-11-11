using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public static WorldManager Instance;

    void OnEnable()
    {
        Instance = this;
    }


}

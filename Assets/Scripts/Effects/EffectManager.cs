using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{

    public List<EffectObject> effectObjects;

    public static EffectManager Instance;

    private ObjectPool objectPool;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        objectPool = ObjectPool.Instance;
    }

    public void Play(EffectType effectType, Vector2 position)
    {
        foreach (EffectObject effectObject in effectObjects)
        {
            if (effectObject.effectType == effectType)
            {
                GameObject gameObject = objectPool.GetObject(effectObject.name, true);

                gameObject.transform.position = position;

                return;
            }
        }
        
    }

    public enum EffectType
    {
        EXPLOSION, DEATH
    }
}

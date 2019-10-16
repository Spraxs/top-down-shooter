using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    [SerializeField] private GameObject soundEffectPrefab;
    [SerializeField] private ObjectPool objectPool;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlaySoundEffect(AudioClip audioClip, Vector2 position, float volume, float pitch, float minDistance, float maxDistance)
    {
        if (volume > 1) volume = 1;
        if (volume < 0) volume = 0;

        if (pitch > 3) pitch = 3;
        if (pitch < -3) pitch = -3;

        if (minDistance < 0) minDistance = 0;
        if (minDistance > maxDistance)
        {
            minDistance = maxDistance - 0.005f;

            if (minDistance < 0)
            {
                minDistance = 0;
                maxDistance = 0.01f;
            }
        }

        GameObject soundObject = objectPool.GetObject(soundEffectPrefab.name);
        soundObject.transform.position = position;

        AudioSource audioSource = soundObject.GetComponent<AudioSource>();

        audioSource.volume = volume;
        audioSource.pitch = pitch;

        audioSource.minDistance = minDistance;
        audioSource.maxDistance = maxDistance;

        audioSource.PlayOneShot(audioClip);


        StartCoroutine(SoundFinished(audioSource, soundObject));

    }

    private IEnumerator SoundFinished(AudioSource audioSource, GameObject soundObject)
    {
        yield return new WaitWhile(() => audioSource.isPlaying);

        objectPool.PoolObject(soundObject);
    }
}

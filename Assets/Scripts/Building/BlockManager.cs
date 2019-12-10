using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    private ConcurrentDictionary<BlockObject, GameObject> placedBlocks = new ConcurrentDictionary<BlockObject, GameObject>();

    [SerializeField] private SoundEffectManager soundEffectManager;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        foreach (BlockObject block in placedBlocks.Keys)
        {
            if (block.lifeTime.Ticks < DateTime.Now.Ticks)
            {
                DestroyBlock(block);
            }
        }
    }

    public void DestroyBlock(BlockObject block)
    {
        GameObject gameObject;

        placedBlocks.TryRemove(block, out gameObject);

        soundEffectManager.PlaySoundEffect(block.blockBreak, gameObject.transform.position, .05f, 1f, 0f, 2f);

        Destroy(gameObject);
    }

    public void SpawnBlock(GameObject blockPrefab, Vector2 position)
    {
        if (IsBlockPlacedOnPosition(position))
        {
            return;
        }

        GameObject blockObject = Instantiate(blockPrefab, position, blockPrefab.transform.rotation);

        BlockObject defaultBlock = blockPrefab.GetComponent<Block>().block;

        BlockObject block = ScriptableObject.CreateInstance<BlockObject>();

        block.blockBreak = defaultBlock.blockBreak; // is necessary?
        block.blockPlace = defaultBlock.blockPlace; // is necessary?

        block.lifeTime = DateTime.Now.AddSeconds(defaultBlock.lifeDurationInSeconds);
        block.health = defaultBlock.health;
        block.SetPlacedBlock(null);;

        placedBlocks.TryAdd(block, blockObject);

        soundEffectManager.PlaySoundEffect(block.blockPlace, gameObject.transform.position, .3f, 1f, 0f, 2f);
    }

    public bool IsBlockPlacedOnPosition(Vector2 position)
    {
        foreach (GameObject blockGameObject in placedBlocks.Values)
        {
            if ((Vector2)blockGameObject.transform.position == position) return true;
        }

        return false;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Block", menuName = "Block")]
public class BlockObject : ScriptableObject
{

    public float health;
    public float lifeDurationInSeconds;
    public AudioClip blockPlace;
    public AudioClip blockBreak;

    private Player placedBlock;

    public void SetPlacedBlock(Player player)
    {
        placedBlock = player;
    }

    public Player GetPlacedBlock()
    {
        return placedBlock;
    }

    public DateTime lifeTime;


    public void SetLifeTime(DateTime lifeTime)
    {
        this.lifeTime = lifeTime;
    }

    public DateTime GetLiteTime()
    {
        return lifeTime;
    }
}

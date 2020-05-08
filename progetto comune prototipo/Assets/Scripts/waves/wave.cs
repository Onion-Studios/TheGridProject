using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wave 
{
    public int[] enemyID;
    public int[] Signgroup;
    public int[] lanes;
    public float[] delays;

    public wave(int[] EnemyID, int[] SignGroup, int[] Lanes, float[] Delays)
    {
        this.enemyID = EnemyID;
        this.Signgroup = SignGroup;
        this.lanes = Lanes;
        this.delays = Delays;
    }

    public wave() { }
}

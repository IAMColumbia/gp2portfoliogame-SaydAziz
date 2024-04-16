using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trinket : MonoBehaviour
{
    protected bool canShoot = true;
    protected int trinketLevel = 1;

    public abstract GameObject GetShadow();
    public abstract int GetCoinPrice();
    public abstract int GetWoodPrice();
    public void Upgrade()
    {
        Debug.Log("UPGRADED");
        trinketLevel++;
    }
    public int GetLevel()
    {
        return trinketLevel;
    }
    protected virtual void ResetShot()
    {
        canShoot = true;
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trinket : MonoBehaviour
{
    protected bool canShoot = true;
    protected int trinketLevel = 1;

    public abstract GameObject GetShadow();
    public abstract int GetCoinUpgradePrice();
    public abstract int GetWoodUpgradePrice();
    public abstract int GetCoinPrice();
    public abstract int GetWoodPrice();
    public abstract string GetName();
    public abstract void Upgrade();
    public abstract int GetMaxLevel();
    public int GetLevel()
    {
        return trinketLevel;
    }
    protected virtual void ResetShot()
    {
        canShoot = true;
    }

}

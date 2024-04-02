using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trinket : MonoBehaviour
{
    protected bool canShoot = true;

    public abstract GameObject GetShadow();
    public abstract int GetCoinPrice();
    public abstract int GetWoodPrice();
    protected virtual void ResetShot()
    {
        canShoot = true;
    }
}

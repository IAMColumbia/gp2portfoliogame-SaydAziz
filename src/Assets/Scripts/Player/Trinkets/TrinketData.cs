using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TrinketData : ScriptableObject
{
    public string trinketName;
    public int trinketCoinPrice;
    public int trinketWoodPrice;
    public float fireRate;
    public GameObject shadowPrefab;
}

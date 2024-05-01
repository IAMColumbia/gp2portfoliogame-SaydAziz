using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TrinketData : ScriptableObject
{
    public string trinketName;
    public int trinketCoinUpgradePrice;
    public int trinketWoodUpgradePrice;
    public int trinketCoinPrice;
    public int trinketWoodPrice;
    public int maxLevel;
    public float fireRate;
    public GameObject shadowPrefab;
}

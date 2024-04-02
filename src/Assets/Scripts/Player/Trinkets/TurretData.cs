using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/TurretData")]
public class TurretData : TrinketData 
{
    public float trinketRange;
    public GameObject bulletPrefab;
    public int damage;
    public LayerMask targetMask;
}

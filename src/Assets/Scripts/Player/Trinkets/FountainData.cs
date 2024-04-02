using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/FountainData")]
public class FountainData : TrinketData 
{
    public float trinketRange;
    public int effectAmount;
    public LayerMask targetMask;
}

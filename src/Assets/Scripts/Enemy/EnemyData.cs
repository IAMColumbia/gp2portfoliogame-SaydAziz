using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/EnemyData")]
public class EnemyData : ScriptableObject 
{

    public int damage;
    public float spawnCost;
    public float health;
    public float attackCooldown;
    public float modelTimer;
    public float attackRange;
    public float moveSpeed;
}

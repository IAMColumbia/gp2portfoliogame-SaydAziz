using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamageable
{
    public float health { get; private set; }

    private void Start()
    {
        health = 100;
    }

    private void FixedUpdate()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("OUCH!!!");
        health -= damage;
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }
}

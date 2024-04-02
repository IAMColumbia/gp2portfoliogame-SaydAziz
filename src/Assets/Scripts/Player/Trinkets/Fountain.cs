using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fountain : Trinket 
{

    [SerializeField] protected SphereCollider effectRange;
    protected Player target;

    [SerializeField] protected FountainData data;
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, data.trinketRange);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered");
        target = other.GetComponent<Player>();
    }
    private void OnTriggerExit(Collider other)
    {
        target = null;
    }

    private void Start()
    {
        effectRange.radius = data.trinketRange;
    }

    private void FixedUpdate()
    { 
        if (target != null)
        {
            if (canShoot)
            {
                ApplyEffect();
            }
        }
    }
    protected virtual void ApplyEffect()
    {
        target.TakeDamage(-data.effectAmount);
        canShoot = false;
        Invoke("ResetShot", data.fireRate);
    }

    public override GameObject GetShadow()
    {
        return data.shadowPrefab;
    }
    public override int GetCoinPrice()
    {
        return data.trinketCoinPrice;
    }
    public override int GetWoodPrice()
    {
        return data.trinketWoodPrice;
    }
}

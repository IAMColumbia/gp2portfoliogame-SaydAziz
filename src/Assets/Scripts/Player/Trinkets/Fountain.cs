using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fountain : Trinket 
{

    [SerializeField] protected SphereCollider effectRange;
    protected Player target;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, ((FountainData)data).trinketRange);
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
        effectRange.radius = ((FountainData)data).trinketRange;
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
        if (((FountainData)data).effectHealth)
        {
            target.TakeDamage(-((FountainData)data).effectAmount);
        }
        if (((FountainData)data).effectCoin)
        {
            target.AddCoin(((FountainData)data).effectAmount);
        }
        if (((FountainData)data).effectWood)
        {
            target.AddWood(((FountainData)data).effectAmount);
        }
        canShoot = false;
        Invoke("ResetShot", data.fireRate / trinketLevel);
    }

    public override GameObject GetShadow()
    {
        return data.shadowPrefab;
    }
    public override int GetMaxLevel()
    {
        return data.maxLevel;
    }
    public override void Upgrade()
    {
        trinketLevel++;
        Debug.Log("UPGRADED");
    }
    public override string GetName()
    {
        return data.trinketName;
    }
    public override int GetCoinUpgradePrice()
    {
        return data.trinketCoinUpgradePrice;
    }
    public override int GetWoodUpgradePrice()
    {
        return data.trinketWoodUpgradePrice;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Trinket 
{
    [SerializeField] protected GameObject turretPivot;
    [SerializeField] protected Transform bulletSpawn;

    protected GameObject target;
    private Vector3 targetPos;
    [SerializeField] private Collider[] shootQueue;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, ((TurretData)data).trinketRange);
    }

    void FixedUpdate()
    {

        if (target == null)
        {
            shootQueue = Physics.OverlapSphere(transform.position, ((TurretData)data).trinketRange, ((TurretData)data).targetMask);
        }
        else if (target.activeInHierarchy == false)
        {
            target = null;
            shootQueue[0] = null;
            return;
        }

        if (shootQueue.Length > 0)
        {
            target = shootQueue[0].gameObject;
            targetPos = new Vector3(target.transform.position.x, turretPivot.transform.position.y, target.transform.position.z);
            turretPivot.transform.LookAt(targetPos);

            if (canShoot)
            {
                Shoot();
            }
        }
    }

    protected virtual void Shoot()
    {
        GameObject currentBullet = Instantiate(((TurretData)data).bulletPrefab, bulletSpawn);
        currentBullet.GetComponent<Projectile>().Initialize(target, ((TurretData)data).damage);
        canShoot = false;
        Invoke("ResetShot", (data.fireRate / trinketLevel) * 0.1f);
    }

    public override GameObject GetShadow()
    {
        return data.shadowPrefab;
    }
    public override int GetMaxLevel()
    {
        return data.maxLevel;
    }
    public override string GetName()
    {
        return data.trinketName;
    }
    public override void Upgrade()
    {
        if (trinketLevel != data.maxLevel)
        {
            trinketLevel++;
            Debug.Log("UPGRADED");
        }
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
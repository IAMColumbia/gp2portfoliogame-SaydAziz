using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Trinket 
{
    [SerializeField] protected GameObject turretPivot;
    [SerializeField] protected Transform bulletSpawn;
    
    [SerializeField] protected TurretData data;


    protected GameObject target;
    private Vector3 targetPos;
    [SerializeField] private Collider[] shootQueue;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, data.trinketRange);
    }

    void FixedUpdate()
    {

        if (target == null)
        {
            shootQueue = Physics.OverlapSphere(transform.position, data.trinketRange, data.targetMask);
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
        GameObject currentBullet = Instantiate(data.bulletPrefab, bulletSpawn);
        currentBullet.GetComponent<Projectile>().Initialize(target, data.damage);
        canShoot = false;
        Invoke("ResetShot", (data.fireRate / trinketLevel) * 0.1f);
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
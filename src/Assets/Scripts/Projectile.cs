using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    GameObject targetEnemy;
    int projectileSpeed = 60;
    int projectileDamage;
    float lifeTime = 3;

    // Start is called before the first frame update
    void Start()
    {
        transform.parent = null;
        Invoke("SelfDestruct", lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (targetEnemy != null)
        {
            if (other.gameObject == targetEnemy)
            {
                DoHit(other.GetComponent<Enemy>());
            }
        }
        else
        {
            DoHit(other.GetComponent<Enemy>());
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (targetEnemy != null)
        {
            if (targetEnemy.activeInHierarchy == false)
            {
                targetEnemy = null;
                return;
            }
            Vector3 targetLoc = new Vector3(targetEnemy.transform.position.x, transform.position.y, targetEnemy.transform.position.z);
            transform.LookAt(targetLoc);
            rb.velocity = transform.forward * projectileSpeed;

        }
    }

    public void Initialize(GameObject target, int damage)
    {
        targetEnemy = target;
        projectileDamage = damage;
    }
    
    void DoHit(Enemy enemy)
    {
        enemy.TakeDamage(projectileDamage);
        Destroy(this.gameObject);
    }
    void SelfDestruct()
    {
        Destroy(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Enemy: MonoBehaviour, IDamageable
{
    [SerializeField] NavMeshAgent agent;
    LayerMask playerMask;

    //Stats
    float health;
    float damage;
    float attackCooldown;
    float attackRange;

    //function
    bool canAttack = true;
    bool playerInRange = false;
    
    void Awake()
    {
        playerMask = LayerMask.GetMask("Player");

        health = 100;
        damage = 20;
        attackCooldown = 1;
        attackRange = 1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z) + transform.forward, new Vector3(1f, 1f, 1f));
    }

    private void FixedUpdate()
    {
        playerInRange = Physics.CheckSphere(transform.position, attackRange, playerMask); 
        if (!playerInRange)
        {
            agent.SetDestination(GameManager.Instance.GetPlayerLocation());
        }
        else
        {

            agent.SetDestination(transform.position);
            Attack(); 

        }
        
    }

    void Attack()
    {

        if (canAttack)
        {
            RaycastHit damageReciever;
            canAttack = false;
            if (Physics.BoxCast(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), new Vector3(0.5f, 0.5f, 0.5f), transform.forward, out damageReciever, transform.rotation, attackRange, playerMask))
            {
                
                IDamageable reciever = damageReciever.collider.gameObject.GetComponent<IDamageable>();
                if (reciever != null)
                {
                    reciever.TakeDamage(damage);
                }
            }
            Invoke("ResetCooldown", attackCooldown);

        }
    }

    void ResetCooldown()
    {
        canAttack = true;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }
}

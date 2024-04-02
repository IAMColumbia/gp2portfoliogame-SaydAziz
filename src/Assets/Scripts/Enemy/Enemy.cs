using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Enemy: MonoBehaviour, IDamageable
{
    [SerializeField] NavMeshAgent agent;
    LayerMask playerMask;

    public PlayerBeacon player;

    //Stats
    [SerializeField] float health;
    [SerializeField] int damage;
    [SerializeField] float attackCooldown;
    [SerializeField] float attackRange;

    //function
    bool canAttack = true;
    bool playerInRange = false;
    
    void Awake()
    {
        playerMask = LayerMask.GetMask("Player");
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z) + transform.forward, new Vector3(1f, 1f, 1f));
    }

    Vector3 playerLoc;
    private void FixedUpdate()
    {
        if (health <= 0 )
        {
            Die();
        }

        playerInRange = Physics.CheckSphere(transform.position, attackRange, playerMask); 

        if (!playerInRange)
        {
            if (agent.isActiveAndEnabled)
            {
                playerLoc.x = player.locX;
                playerLoc.z = player.locY;
                agent.SetDestination(playerLoc);
            } 
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

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    public void Die()
    {
        GameManager.Instance.AddCoin(1);
        this.gameObject.SetActive(false);
        health = 100;
    }
}

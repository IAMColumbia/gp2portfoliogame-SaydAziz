using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Enemy: MonoBehaviour, IDamageable
{
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected GameObject attackModel;
    protected LayerMask playerMask;

    public PlayerBeacon player;

    //Stats
    [SerializeField] protected float health;
    [SerializeField] protected int damage;
    [SerializeField] protected float attackCooldown;
    [SerializeField] protected float modelTimer;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float moveSpeed;


    //function
    protected bool canAttack = true;
    protected bool playerInRange = false;
    
    void Awake()
    {
        playerMask = LayerMask.GetMask("Player");
    }
    void Start()
    {
        agent.speed = moveSpeed;
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

    protected virtual void Attack()
    {

        if (canAttack)
        {
            RaycastHit damageReciever;
            canAttack = false;
            attackModel.SetActive(true);
            if (Physics.BoxCast(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), new Vector3(0.5f, 0.5f, 0.5f), transform.forward, out damageReciever, transform.rotation, attackRange, playerMask))
            {
                 
                IDamageable reciever = damageReciever.collider.gameObject.GetComponent<IDamageable>();
                if (reciever != null)
                {
                    reciever.TakeDamage(damage);
                }
            }
            Invoke("ResetCooldown", attackCooldown);
            Invoke("ModelHide", modelTimer);

        }
    }

    void ResetCooldown()
    {
        canAttack = true;
    }
    void ModelHide()
    {
        attackModel.SetActive(false);
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

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player: UISubject, IDamageable
{

    public int health { get; private set; }
    public int coins { get; private set; }
    public int wood { get; private set; }

    public PlayerBeacon playerBeacon{ get; private set; }
    public Vector3 cursorPos { get; set; }

    private void Awake()
    {
        playerBeacon = new PlayerBeacon();
    }

    private void Start()
    {
        health = 100;
        NotifyUIObservers(0, health);
        coins = 100;
        NotifyUIObservers(2, coins);
        wood = 20;
        NotifyUIObservers(3, wood);
    }

    private void FixedUpdate()
    {
        if (health <= 0)
        {
            Die();
        }

        playerBeacon.locX = transform.position.x;
        playerBeacon.locY = transform.position.z;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("OUCH!!!");
        health -= damage;
        health = Mathf.Clamp(health, 0, 100);
        NotifyUIObservers(0, health);
    }

    public void AddCoin(int amount)
    {
        coins += amount;
        NotifyUIObservers(2, coins);
    }

    public void AddWood(int amount)
    {
        wood += amount;
        NotifyUIObservers(3, wood);
    }
    public void SelectTrinket(int selection)
    {
        NotifyObservers(selection);
    }

    public void Purchase()
    {
        NotifyObservers(0);
    }

    public void Die()
    {
        NotifyObservers(30);
        Destroy(this.gameObject);
    }
}

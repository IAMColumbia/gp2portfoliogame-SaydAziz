using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerState
{
    Normal,
    Trinket, 
    Dead
}

public class Player: UISubject, IDamageable
{

    public int health { get; private set; }
    public int coins { get; private set; }
    public int wood { get; private set; }

    public PlayerBeacon playerBeacon{ get; private set; }
    public Ray mouseRay { get; set; }
    public PlayerState currentState { get; private set; }
    public Trinket focusedTrinket { get; private set; }

    [SerializeField] LayerMask trinketLayer;
    

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

        currentState = PlayerState.Normal;
        NotifyObservers(currentState);
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
        currentState = PlayerState.Trinket;
        NotifyUIObservers(10, 1);
        NotifyObservers(currentState);
        NotifyObservers(selection);
    }

    public void TriggerNormalMode()
    {
        currentState = PlayerState.Normal;
        NotifyObservers(currentState);
        NotifyUIObservers(10, 0);
    }

    public void Purchase()
    {
        NotifyObservers(0);
    }

    public void UpgradeTrinket()
    {
        NotifyObservers(40);
        NotifyUIObservers(focusedTrinket);
    }

    public void ViewTrinket(Camera cam)
    {
        RaycastHit hit;
        Physics.Raycast(mouseRay, out hit, 100, trinketLayer);

        if (hit.collider != null)
        {
            focusedTrinket = hit.collider.gameObject.GetComponent<Trinket>();
            GameManager.Instance.focusedTrinket = focusedTrinket;
            NotifyUIObservers(focusedTrinket);
        }
        //else
        //{
        //    focusedTrinket = null;
        //    NotifyUIObservers(11, 0);
        //}
    }

    public void Die()
    {
        currentState = PlayerState.Dead;
        NotifyObservers(currentState);
        Destroy(this.gameObject);
    }
}

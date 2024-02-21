using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] PlayerController player;
    [SerializeField] TrinketManager placer;


    public static GameManager Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaceTrinket(int trinketIndex, float x, float y, float z)
    {
        Vector3 location = new Vector3(x, y, z);
        placer.Place(trinketIndex, location); 
    }

    public Vector3 GetPlayerLocation()
    {
        return player.transform.position;
    }
}

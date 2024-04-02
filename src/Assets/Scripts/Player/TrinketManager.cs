using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrinketManager : MonoBehaviour, IObserver
{
    Player player;
    [SerializeField] GameObject[] trinketPrefabs;
    [SerializeField] GameObject[] trinketShadows;
    [SerializeField] Grid grid;
    Camera cam;
    LayerMask placeLayer;

    //Trinket values
    int selectedTrinket;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<Player>();
        cam = Camera.main;
        player.AddObserver(this);

        for (int i = 0; i < trinketPrefabs.Length; i++)
        {
            trinketShadows[i] = Instantiate(trinketPrefabs[i].GetComponent<Trinket>().GetShadow(), this.gameObject.transform);
            trinketShadows[i].SetActive(false);
        }

        trinketShadows[0].SetActive(true);

        selectedTrinket = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3Int gridPos = grid.WorldToCell(player.cursorPos);
        trinketShadows[selectedTrinket].transform.position = grid.GetCellCenterWorld(gridPos);
    }


    public void SelectTrinket(int trinket)
    {
        trinketShadows[selectedTrinket].SetActive(false);
        selectedTrinket = trinket - 1;
        trinketShadows[selectedTrinket] = trinketShadows[trinket - 1];
        trinketShadows[selectedTrinket].SetActive(true);
    }

    public void Place()
    {
        int coin = trinketPrefabs[selectedTrinket].GetComponent<Trinket>().GetCoinPrice();
        int wood = trinketPrefabs[selectedTrinket].GetComponent<Trinket>().GetWoodPrice();
        if (player.coins - coin >= 0 && player.wood - wood >= 0)
        {
            player.AddCoin(-coin);
            player.AddWood(-wood);
            Instantiate(trinketPrefabs[selectedTrinket], trinketShadows[selectedTrinket].transform.position, UnityEngine.Quaternion.identity);
        }
    }

    public void OnNotify(int id)
    {
        if (id == 0) 
        {
            Place();
            return;
        }
        else if (id < 5)
        {
            SelectTrinket(id);
        }
    }
}

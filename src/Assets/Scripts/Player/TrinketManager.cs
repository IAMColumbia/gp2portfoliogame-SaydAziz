using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrinketManager : MonoBehaviour, IObserver
{
    Player player;
    PlayerState pState;
    LayerMask placeLayer;
    LayerMask enviroLayer;

    [SerializeField] GameObject[] trinketPrefabs;
    [SerializeField] GameObject[] trinketShadows;
    [SerializeField] Grid grid;

    Vector3 cursorPos;

    //Trinket values
    int selectedTrinket;
    bool unObstructed;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<Player>();
        player.AddObserver(this);

        placeLayer = LayerMask.GetMask("Place");
        enviroLayer = LayerMask.GetMask("Environment");

        for (int i = 0; i < trinketPrefabs.Length; i++)
        {
            trinketShadows[i] = Instantiate(trinketPrefabs[i].GetComponent<Trinket>().GetShadow(), this.gameObject.transform);
            trinketShadows[i].SetActive(false);
        }

        trinketShadows[0].SetActive(true);

        selectedTrinket = 0;
        unObstructed = true;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Physics.Raycast(player.mouseRay, out hit, 100, placeLayer);

        cursorPos = hit.point;

        Vector3Int gridPos = grid.WorldToCell(cursorPos);

        Collider[] Overlaps = Physics.OverlapBox(grid.GetCellCenterWorld(gridPos), new Vector3(0.5f, 0.5f, 0.5f), Quaternion.identity, enviroLayer);

        if (pState == PlayerState.Trinket)
        {
            if (Overlaps.Length == 0)
            {
                unObstructed = true;
                trinketShadows[selectedTrinket].SetActive(true);
                trinketShadows[selectedTrinket].transform.position = grid.GetCellCenterWorld(gridPos);
            }
            else
            {
                unObstructed = false;
                trinketShadows[selectedTrinket].SetActive(false);
            }
        }
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
        if (player.coins - coin >= 0 && player.wood - wood >= 0 && unObstructed)
        {
            player.AddCoin(-coin);
            player.AddWood(-wood);
            Instantiate(trinketPrefabs[selectedTrinket], trinketShadows[selectedTrinket].transform.position, UnityEngine.Quaternion.identity);
        }
    }
    public void Upgrade()
    {
        var trinket = GameManager.Instance.focusedTrinket;
        int coin = trinketPrefabs[selectedTrinket].GetComponent<Trinket>().GetCoinUpgradePrice();
        int wood = trinketPrefabs[selectedTrinket].GetComponent<Trinket>().GetWoodUpgradePrice();
        if (player.coins - coin >= 0 && player.wood - wood >= 0 && trinket.GetLevel() < trinket.GetMaxLevel())
        {
            player.AddCoin(-coin);
            player.AddWood(-wood);
            trinket.Upgrade();
        }
    }

    private void StateSwitch()
    {
        if (pState == PlayerState.Trinket)
        {
            trinketShadows[selectedTrinket].SetActive(true);
        }
        else
        {
            trinketShadows[selectedTrinket].SetActive(false);
        }
    }

    public void OnNotify(int id)
    {
        if (pState == PlayerState.Trinket)
        {
            if (id == 0)
            {
                Place();
            }
            else if (id < 5)
            {
                SelectTrinket(id);
            }
        }
        else if (pState == PlayerState.Normal)
        {
            if (id == 40)
            {
                Upgrade();
            }

        }
    }
    public void OnNotify(PlayerState state)
    {
        pState = state;
        StateSwitch();
    }

}

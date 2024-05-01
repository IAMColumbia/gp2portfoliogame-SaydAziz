using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, IUIObserver
{
    [SerializeField] Player player;
    [SerializeField] WaveManager waveManager;

    [SerializeField] TMP_Text coins;
    [SerializeField] TMP_Text wood;
    [SerializeField] TMP_Text wave;
    [SerializeField] TMP_Text highScore;
    [SerializeField] Slider healthBar;


    [SerializeField] GameObject trinketMenu;
    [SerializeField] GameObject trinketFocus;

    //TrinketFocus
    [SerializeField] TMP_Text trinketLevel;
    [SerializeField] TMP_Text trinketName;

    public void NotifyUI(int id, int content)
    {
        switch (id)
        {
            case 0:
                healthBar.value = content;
                break;
            case 1:
                wave.text = "Wave: " + content;
                break;
            case 2:
                coins.text = "Coins: " + content;
                break;
            case 3:
                wood.text = "Wood: " + content;
                break;
            case 4:
                highScore.text = "High Score: " + content;
                break;
            case 10:
                trinketMenu.SetActive(Convert.ToBoolean(content));
                trinketFocus.SetActive(false); 
                break;
;           //case 11:
        }
    }

    public void NotifyUI(Trinket trinket)
    {
        trinketName.text = trinket.GetName();
        if (trinket.GetLevel() != trinket.GetMaxLevel())
        {
            trinketLevel.text = "Level " + trinket.GetLevel();
        }
        else
        {
            trinketLevel.text = "Level MAX";
        }
            trinketFocus.SetActive(true);
    }


    // Start is called before the first frame update
    void Awake()
    {
        player.AddUIObserver(this);
        waveManager.AddUIObserver(this);
    }
    void Start()
    {
        highScore.text = " ";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

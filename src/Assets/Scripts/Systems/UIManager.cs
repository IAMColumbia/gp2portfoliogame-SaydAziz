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
        }
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

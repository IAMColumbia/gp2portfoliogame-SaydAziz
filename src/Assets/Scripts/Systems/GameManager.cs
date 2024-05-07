using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IObserver
{

    [SerializeField] Player player;
    [SerializeField] Menu pauseMenu;
    [SerializeField] WaveManager waveManager;
    public Trinket focusedTrinket;

    public static GameManager Instance;


    //Game State
    bool isPaused = false;
    int highScore;

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

    private void Start()
    {
        player.AddObserver(this);
        //QualitySettings.vSyncCount = 1;
        //Application.targetFrameRate = 60;
    }

    public void AddCoin(int value)
    {
        player.AddCoin(value);
    }

    private void EndGame()
    {
        waveManager.DisableWaves();

        Pause();
    }

    public void Pause()
    {
        isPaused = isPaused ? false : true;
        pauseMenu.ShowPauseMenu(isPaused);
    }

    public void UpgradeTrinket()
    {
        player.UpgradeTrinket();

    }



    public void OnNotify(PlayerState state)
    {
        if (state == PlayerState.Dead)
        {
            EndGame();
        }
    }
    public void OnNotify(int id)
    {
    }
}

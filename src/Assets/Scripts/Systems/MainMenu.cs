using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text txtHighScore;

    private void Start()
    {
        txtHighScore.text = "High Score: " + PlayerPrefs.GetInt("High Score").ToString();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using TMPro;

public class EndScreen : MonoBehaviour
{
    public TextMeshProUGUI player1Res;
    public TextMeshProUGUI player2Res;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(GlobalDataManager.Instance.player1Food);

        player1Res.text = "Player One Collected: " + GlobalDataManager.Instance.player1Food + " pieces of food";
        player2Res.text = "Player Two Collected: " + GlobalDataManager.Instance.player2Food + " pieces of food";

    }


    public void goToMenu()
    {
        GlobalDataManager.Instance.player1Food = 0;
        GlobalDataManager.Instance.player2Food = 0;
        GlobalDataManager.Instance.player1Health = GlobalDataManager.Instance.p1MaxHealth;
        GlobalDataManager.Instance.player2Health = GlobalDataManager.Instance.p2MaxHealth;
        SceneManager.LoadScene("StartScreen");
    }

    public void exitToWindows()
    {
        Application.Quit();
    }
}

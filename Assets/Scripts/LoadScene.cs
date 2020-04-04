using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadScene : MonoBehaviour
{
    public string nextScene;
    private Player player1;
    private Player player2;

    private float waitToLoad = 2f;

    private GameObject nextLevelText;
    private GameObject endGameText;
    private GameObject playersDeadText;

    public bool transitioned = false;
    private bool gameOver = false;

    public void Start()
    {
        nextLevelText = GameObject.FindGameObjectWithTag("NewScene");
        endGameText = GameObject.FindGameObjectWithTag("EndGame");
        playersDeadText = GameObject.FindGameObjectWithTag("PlayersDead");

        endGameText.gameObject.SetActive(false);
        nextLevelText.gameObject.SetActive(false);
        playersDeadText.gameObject.SetActive(false);

        EventSystem.Instance.onLeavingIsland += OnLoadNextScene;
        EventSystem.Instance.OnPlayerDead += OnPlayersDied;

        if (SceneManager.GetActiveScene().name != "EndGame" && SceneManager.GetActiveScene().name != "TransitionScene")
        {
            player1 = GameObject.FindWithTag("Player1").GetComponent<Player>();
            player2 = GameObject.FindWithTag("Player2").GetComponent<Player>();
        }
    }

    public void OnLoadNextScene()
    {
        if (SceneManager.GetActiveScene().name != "EndGame" && SceneManager.GetActiveScene().name != "TransitionScene")
        {
            //Save player 1 data
            player1.SaveData();

            //Save player 2 data
            player2.SaveData();
            GlobalDataManager.Instance.Levels.Remove(SceneManager.GetActiveScene().name);
            if (GlobalDataManager.Instance.Levels.Count == 0)
            {
                endGameText.gameObject.SetActive(true);
            }
            else
            {
                nextLevelText.gameObject.SetActive(true);
            }

        }
        waitToLoad = 2f;
        StartCoroutine(LoadSceneCo());


    }

    public IEnumerator LoadSceneCo()
    {
        yield return new WaitForSecondsRealtime(waitToLoad);

        if (GlobalDataManager.Instance.Levels.Count == 0 || gameOver)
        {
            EventSystem.Instance.onLeavingIsland -= OnLoadNextScene;
            SceneManager.LoadScene("EndGame");
        } else if (GlobalDataManager.Instance.Levels.Count != 0)
        {
            EventSystem.Instance.onLeavingIsland -= OnLoadNextScene;

            SceneManager.LoadScene("TransitionScene");
        }
    }

    public void OnPlayersDied(int ID)
    {
        if ((!player1.gameObject.activeSelf && ID == 2) || (!player2.gameObject.activeSelf && ID ==1))
        {
            //GlobalDataManager.Instance.ResetData();
            waitToLoad = 2f;
            gameOver = true;
            playersDeadText.gameObject.SetActive(true);
            StartCoroutine(LoadSceneCo());

        }
    }
    private void OnDestroy()
    {
        EventSystem.Instance.OnPlayerDead -= OnPlayersDied;
    }
}
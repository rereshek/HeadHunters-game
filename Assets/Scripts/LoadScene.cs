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

    public bool transitioned = false;

    public void Start()
    {
        nextLevelText = GameObject.FindGameObjectWithTag("NewScene");
        endGameText = GameObject.FindGameObjectWithTag("EndGame");

        endGameText.gameObject.SetActive(false);
        nextLevelText.gameObject.SetActive(false);

        EventSystem.Instance.onLeavingIsland += OnLoadNextScene;
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
        Debug.Log("Current scene: " + SceneManager.GetActiveScene().name);
        yield return new WaitForSecondsRealtime(waitToLoad);

        if (GlobalDataManager.Instance.Levels.Count != 0)
        {
            Debug.Log("Loading transition from" + SceneManager.GetActiveScene().name);
            EventSystem.Instance.onLeavingIsland -= OnLoadNextScene;

            SceneManager.LoadScene("TransitionScene");
        }
        else if (GlobalDataManager.Instance.Levels.Count == 0)
        {
            Debug.Log("End game from " + SceneManager.GetActiveScene().name);
            EventSystem.Instance.onLeavingIsland -= OnLoadNextScene;
            SceneManager.LoadScene("EndGame");
        }
    }
    private void OnDestroy()
    {
        //EventSystem.Instance.onLeavingIsland -= OnLoadNextScene;
    }
}
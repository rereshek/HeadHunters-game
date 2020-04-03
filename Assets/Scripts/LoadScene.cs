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

    public TextMeshProUGUI nextLevelText;

    public bool transitioned = false;

    public void Start()
    {
        Debug.Log("Called LoadScene::Start");
        EventSystem.Instance.onLeavingIsland += OnLoadNextScene;
        if(SceneManager.GetActiveScene().name != "EndGame" && SceneManager.GetActiveScene().name != "TransitionScene")
        {            
            player1 = GameObject.FindWithTag("Player1").GetComponent<Player>();
            player2 = GameObject.FindWithTag("Player2").GetComponent<Player>();
        }
    }

    public void OnLoadNextScene()
    {
        Debug.Log("********LOADING NEXT SCENE: " + SceneManager.GetActiveScene().name + " ************");
        if (SceneManager.GetActiveScene().name != "EndGame" && SceneManager.GetActiveScene().name != "TransitionScene")
        {
            //Save player 1 data
            player1.SaveData();

            //Save player 2 data
            player2.SaveData();

            nextLevelText.gameObject.SetActive(true);
        }
        //if(SceneManager.GetActiveScene().name == "TransitionScene")
        //{
        //    waitToLoad = 18f;
        //}
        //if (SceneManager.GetActiveScene().name.Contains("Level"))
        //{
        waitToLoad = 2f;
        //}
        //Load the next scene
        StartCoroutine(LoadSceneCo());


    }

    public IEnumerator LoadSceneCo()
    {
        Debug.Log("Current scene: " + SceneManager.GetActiveScene().name);
        yield return new WaitForSecondsRealtime(waitToLoad);
        //if(transitioned && SceneManager.GetActiveScene().name == "TransitionScene" && SceneManager.GetActiveScene().name != "EndGame" && GlobalDataManager.Instance.Levels.Count != 0)
        if (GlobalDataManager.Instance.Levels.Count != 0)
        { 
            Debug.Log("Loading transition from" + SceneManager.GetActiveScene().name);
            EventSystem.Instance.onLeavingIsland -= OnLoadNextScene;
            GlobalDataManager.Instance.Levels.Remove(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("TransitionScene");
        }
        else if(GlobalDataManager.Instance.Levels.Count == 0)
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadScene : MonoBehaviour
{
	public string nextScene;
	public Player player1;
	public Player player2;

    public float waitToLoad = 2f;

    public TextMeshProUGUI nextLevelText;

    public void Start()
	{
        //What needs to be done here
        //
        EventSystem.Instance.onLeavingIsland += OnLoadNextScene;
        //player1 = GameObject.FindWithTag("Player1").GetComponent<Player>();
        //player2 = GameObject.FindWithTag("Player2").GetComponent<Player>();

    }
	
	public void OnLoadNextScene()
	{
        Debug.Log("OnLoadNextScene");
        //Save player 1 data
		player1.SaveData();
		
		//Save player 2 data
		player2.SaveData();

        nextLevelText.gameObject.SetActive(true);

        //Load the next scene
        StartCoroutine("LoadSceneCo");


    }


    public IEnumerator LoadSceneCo()
    {
        yield return new WaitForSeconds(waitToLoad);
        EventSystem.Instance.onLeavingIsland -= OnLoadNextScene;
        SceneManager.LoadScene(nextScene);

    }

}
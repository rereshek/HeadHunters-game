using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutScene : MonoBehaviour
{
    private FoodUIData foodUI;
    private MinionUIData minionUI;
    // Start is called before the first frame update
    void Start()
    {
        UpdateUI(1);
        UpdateUI(2);
        StartCoroutine(LoadLevelCo());
        //EventSystem.Instance.LeavingIsland();

    }

    

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateUI(int playerID)
    {
        
        foodUI.playerID = playerID;
        minionUI.playerID = playerID;
        if (playerID == 1)
        {
            foodUI.foodCount = GlobalDataManager.Instance.player1Food;
            minionUI.minionCount = GlobalDataManager.Instance.p1MinionCount;
           

        }
        if (playerID == 2)
        {
            foodUI.foodCount = GlobalDataManager.Instance.player2Food;
            minionUI.minionCount = GlobalDataManager.Instance.p2MinionCount;
        }
        //Debug.Log("Food UI " + playerID + " " + foodUI.foodCount);
        EventSystem.Instance.UpdateMinionCount(minionUI);
        EventSystem.Instance.FoodCollected(foodUI);
    }

    IEnumerator waitAMo()
    {
        yield return new WaitForSeconds(2f);
    }

    public IEnumerator LoadLevelCo()
    {
        string nextScene;

        Debug.Log("Current scene: " + SceneManager.GetActiveScene().name);
        yield return new WaitForSecondsRealtime(18f);

        if (GlobalDataManager.Instance.Levels.Count > 0)
        {
            //Debug.Log("Loading next Level from " + SceneManager.GetActiveScene().name);
            int i = Random.Range(0, GlobalDataManager.Instance.Levels.Count);
            nextScene = GlobalDataManager.Instance.Levels[i];

            SceneManager.LoadScene(nextScene);
        }
        if (GlobalDataManager.Instance.Levels.Count == 0)
        {
            Debug.Log("End game from " + SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("EndGame");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showEUI : MonoBehaviour
{
    public GameObject enemyUI;
    public int enemyID;
    // Start is called before the first frame update
    void Start()
    {
        EventSystem.Instance.OnEnemyCombatChange += showHideEnemyUI;
        enemyUI.SetActive(false);
        enemyID = gameObject.GetComponent<HealthBar>().PlayerID;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showHideEnemyUI (EnemyUIData eUIData)
    {
        
        if(eUIData.enemyID == enemyID)
        {
            enemyUI.SetActive(eUIData.showUI);
        }
    }

    private void OnDestroy()
    {
        EventSystem.Instance.OnEnemyCombatChange -= showHideEnemyUI;
    }
}

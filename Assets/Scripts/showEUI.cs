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

    public void showHideEnemyUI(EnemyUIData eUIData)
    {

        if (eUIData.enemyID == enemyID)
        {
            HealthUIData newHealthData;
            newHealthData.health = eUIData.eHealth;
            newHealthData.playerID = eUIData.enemyID;
            newHealthData.maxHealth = eUIData.eMaxHealth;

            enemyUI.SetActive(eUIData.showUI);

            EventSystem.Instance.HealthChanged(newHealthData);
        }
    }

    private void OnDestroy()
    {
        EventSystem.Instance.OnEnemyCombatChange -= showHideEnemyUI;
    }
}

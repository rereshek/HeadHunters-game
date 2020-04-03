using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MinonListener : MonoBehaviour
{
    public TextMeshProUGUI minion;
    public int playerID;

    // Start is called before the first frame update
    void Start()
    {
        minion = gameObject.GetComponent<TextMeshProUGUI>();
        minion.text = "Minions: 0";
        EventSystem.Instance.OnMinionUIUpdate += OnMinionUpdated;
    }

    private void OnMinionUpdated(MinionUIData minionUI)
    {
        if (playerID == minionUI.playerID)
        {
            minion.text = "Minions: " + minionUI.minionCount.ToString();
        }
    }

    private void OnDestroy()
    {
        EventSystem.Instance.OnMinionUIUpdate -= OnMinionUpdated;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoatControl : MonoBehaviour
{
    public bool p1Safe = false;
    public bool p2Safe = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.CompareTag("Player1"))
        {
            p1Safe = true;
            col.gameObject.SetActive(false);
        }
        if (col.CompareTag("Player2"))
        {
            p2Safe = true;
            col.gameObject.SetActive(false);
        }
        if(p1Safe && p2Safe)
        {
            EventSystem.Instance.LeavingIsland();
        }
    }
}

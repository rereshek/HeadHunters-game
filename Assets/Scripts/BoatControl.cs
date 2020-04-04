using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatControl : MonoBehaviour
{
    public bool p1Safe = false;
    public bool p2Safe = false;

    public bool p1Dead = false;
    public bool p2Dead = false;

    public Sprite p1Boat;
    public Sprite p2Boat;
    public Sprite bothBoat;
    public Sprite boat;

    private SpriteRenderer boatRenderer;
    // Start is called before the first frame update
    void Start()
    {
        boatRenderer = gameObject.GetComponent<SpriteRenderer>();
        gameObject.GetComponent<SpriteRenderer>().sprite = boat;
        EventSystem.Instance.OnPlayerDead += OnPlayerDead;
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
            boatRenderer.sprite = p1Boat;
        }
        if (col.CompareTag("Player2"))
        {
            p2Safe = true;
            col.gameObject.SetActive(false);
            boatRenderer.sprite = p2Boat;
        }
        if((p1Safe && p2Safe) || (p1Dead && p2Safe) || (p1Safe && p2Dead))
        {
            if (p1Dead)
            {
                boatRenderer.sprite = p2Boat;
            }
            if (p2Dead)
            {
                boatRenderer.sprite = p1Boat;
            }
            else if (p1Safe && p2Safe)
            {
                boatRenderer.sprite = bothBoat;
            }
            EventSystem.Instance.LeavingIsland();
        }
    }

    public void OnPlayerDead(int Id)
    {
        Debug.Log("Player Dead");
        if(Id == 1)
        {
            p1Dead = true;
        }
        if(Id == 2)
        {
            p2Dead = true;
        }
        if ((p1Dead && p2Safe) || (p1Safe && p2Dead))
        {
            if (p1Dead)
            {
                boatRenderer.sprite = p2Boat;
            }
            if (p2Dead)
            {
                boatRenderer.sprite = p1Boat;
            }
            
            EventSystem.Instance.LeavingIsland();
        }
    }

    public void OnDestroy()
    {
        EventSystem.Instance.OnPlayerDead -= OnPlayerDead;
    }
}

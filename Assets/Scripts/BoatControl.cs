using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatControl : MonoBehaviour
{
    public bool p1Safe = false;
    public bool p2Safe = false;

    public Sprite p1Boat;
    public Sprite p2Boat;
    public Sprite bothBoat;
    public Sprite boat;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = boat;
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
            gameObject.GetComponent<SpriteRenderer>().sprite = p1Boat;
        }
        if (col.CompareTag("Player2"))
        {
            p2Safe = true;
            col.gameObject.SetActive(false);
            gameObject.GetComponent<SpriteRenderer>().sprite = p2Boat;
        }
        if(p1Safe && p2Safe)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = bothBoat;
            StartCoroutine(waitAMo());
            EventSystem.Instance.LeavingIsland();
        }
    }

    IEnumerator waitAMo ()
    {
        yield return new WaitForSeconds(2.0f);
    }
}

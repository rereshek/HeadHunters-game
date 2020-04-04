using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    private bool isOpen = false;
    public Sprite chestOpened;
    private SpriteRenderer chestRenderer;

    private Player P1;
    private Player P2;
    // Start is called before the first frame update
    void Start()
    {
        chestRenderer = gameObject.GetComponent<SpriteRenderer>();
        P1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<Player>();
        P2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.tag.Contains("Enemy"))
        {
            if (!isOpen)
            {
                chestRenderer.sprite = chestOpened;
                if (collision.CompareTag("Player1"))
                {
                    P1.AddFood(25);
                }
                if (collision.CompareTag("Player2"))
                {
                    P2.AddFood(25);
                }
                isOpen = true;
            }
        }

    }
}

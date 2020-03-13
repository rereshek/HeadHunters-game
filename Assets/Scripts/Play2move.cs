using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play2move : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public GameObject playerSlash;

    //Speed of the player character
    float moveSpeed = 3F;
    float normalizedSpeed = 2.121F;
    float x;
    float y;

    //direction faced, 0=up 1=right 2=down 3=left
    int facedir = 0;

    //attack timer
    float actionTimer;

    // Update is called once per frame
    void Update()
    {
        x = transform.position.x;
        y = transform.position.y;
        actionTimer += Time.deltaTime;
        if (Input.GetKey("up") && Input.GetKey("right"))
        {
            transform.Translate(normalizedSpeed * Time.deltaTime, normalizedSpeed * Time.deltaTime, 0);
        }
        else if (Input.GetKey("down") && Input.GetKey("right"))
        {
            transform.Translate(normalizedSpeed * Time.deltaTime, -normalizedSpeed * Time.deltaTime, 0);
        }
        else if (Input.GetKey("up") && Input.GetKey("left"))
        {
            transform.Translate(-normalizedSpeed * Time.deltaTime, normalizedSpeed * Time.deltaTime, 0);
        }
        else if (Input.GetKey("down") && Input.GetKey("left"))
        {
            transform.Translate(-normalizedSpeed * Time.deltaTime, -normalizedSpeed * Time.deltaTime, 0);
        }
        else
        {
            if (Input.GetKey("up"))
            {
                transform.Translate(0, moveSpeed * Time.deltaTime, 0);
                facedir = 0;
            }
            if (Input.GetKey("down"))
            {
                transform.Translate(0, -moveSpeed * Time.deltaTime, 0);
                facedir = 2;
            }
            if (Input.GetKey("right"))
            {
                transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
                facedir = 1;
            }
            if (Input.GetKey("left"))
            {
                transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
                facedir = 3;
            }
        }
        if (Input.GetKeyDown("[0]"))
        {
            if (actionTimer > 0.3)
            {
                GameObject slashClone;
                slashClone = Instantiate(playerSlash, transform.position, Quaternion.identity);
                slashClone.AddComponent<destroyScript>();
                if (facedir == 0)
                {
                    slashClone.GetComponent<Transform>().position = new Vector2(x, y + 1);
                }
                else if (facedir == 1)
                {
                    slashClone.GetComponent<Transform>().position = new Vector2(x + 1, y);
                }
                else if (facedir == 2)
                {
                    slashClone.GetComponent<Transform>().position = new Vector2(x, y - 1);
                }
                else if (facedir == 3)
                {
                    slashClone.GetComponent<Transform>().position = new Vector2(x - 1, y);
                }
                actionTimer = 0;
            }
        }

    }
}
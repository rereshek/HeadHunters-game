using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play1Move : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    //Speed of the player character
    float moveSpeed = 3F;
    float normalizedSpeed = 2.121F;

    // Update is called once per frame
    void Update()
    {

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
            }
            if (Input.GetKey("down"))
            {
                transform.Translate(0, -moveSpeed * Time.deltaTime, 0);
            }
            if (Input.GetKey("right"))
            {
                transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
            }
            if (Input.GetKey("left"))
            {
                transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
            }
        }

    }
}
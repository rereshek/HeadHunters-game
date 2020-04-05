using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip playerHitSound, playerDeadSound, enemyHitSound, enemySpotSound, enemyDeathSound;
    static AudioSource audioSrc;


    // Start is called before the first frame update
    void Start()
    {
        playerHitSound = Resources.Load<AudioClip>("sword");
        playerDeadSound = Resources.Load<AudioClip>("dead");
        enemyHitSound = Resources.Load<AudioClip>("attack");
        enemySpotSound = Resources.Load<AudioClip>("spot");
        enemyDeathSound = Resources.Load<AudioClip>("death");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "fight":
                audioSrc.PlayOneShot(playerHitSound);
                break;
            case "dead":
                audioSrc.PlayOneShot(playerDeadSound);
                break;
            case "hit":
                audioSrc.PlayOneShot(enemyHitSound);
                break;
            case "spot":
                audioSrc.PlayOneShot(enemySpotSound);
                break;
            case "death":
                audioSrc.PlayOneShot(enemyDeathSound);
                break;


        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    
    // LOAD LEVEL 1 SCENE
    public void PlayGame(){
    	
    	SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
}

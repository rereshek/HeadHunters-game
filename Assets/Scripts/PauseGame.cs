using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseGame : MonoBehaviour
{
    public GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        EventSystem.Instance.OnPauseGame += OnPauseGame;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPauseGame()
    {
        if (!pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void OnDestroy()
    {
        EventSystem.Instance.OnPauseGame -= OnPauseGame;
    }

    public void ReturnToGame()
    {
        if (pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void EndGame()
    {
        GlobalDataManager.Instance.ResetData();
        Time.timeScale = 1;
        SceneManager.LoadScene("StartScreen");
    }
}

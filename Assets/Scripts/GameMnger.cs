using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMnger : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool isGameOver;
    public GameObject gameOverPanel;
    public GameObject pauseMenu;

    void Start()
    {
        isGameOver = false;
        gameOverPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {
            gameOverPanel.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Y) && isGameOver)
            {
                SceneManager.LoadScene("MainGame");
            }
            if (Input.GetKeyDown(KeyCode.Q) && isGameOver)
            {
                GoBackToMenu();
            }
        }
        /*
        if (!isGameOver && Input.GetKeyDown(KeyCode.Escape)) {

            PauseGame();
        
        }*/
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        

    }

    public void PlayGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;

    }
    public void GoBackToMenu()
    {

        SceneManager.LoadScene("Menu"); ;
    }

}

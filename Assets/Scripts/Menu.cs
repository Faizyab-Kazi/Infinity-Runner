using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void PlayGame() {

        SceneManager.LoadScene("MainGame");
    }

    public void QuitGame()
    {

        Application.Quit();
    }

    public void Instructions()
    {

        SceneManager.LoadScene("Instructions"); ;
    }

    public void GoBack()
    {

        SceneManager.LoadScene("Menu"); ;
    }

   

}

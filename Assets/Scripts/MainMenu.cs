using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

    public void Reset()
    {
        SceneManager.LoadScene(3);
    }

    public void MainMenus()
    {
        SceneManager.LoadScene(0);

    }

}


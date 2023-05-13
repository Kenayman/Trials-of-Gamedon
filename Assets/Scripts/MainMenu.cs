using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    private void Start()
    {
        canvas.enabled = false;
    }

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

    public void CreditsLog() 
    {
        canvas.enabled = true;
    }

    public void CreditsOut()
    {
        canvas.enabled = false;
    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    private void Start()
    {
        canvas.enabled = false;
        Time.timeScale = 1.0f;


    }

    public void Continue()
    {
        canvas.enabled = false;
        Time.timeScale = 1.0f;

    }

    public void Pause()
    {
        Time.timeScale = 0f;
        canvas.enabled = true;

    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
        GameObject player = GameObject.Find("Player");
        GameObject camara = GameObject.Find("Camera");
        Destroy(player);
        Destroy(camara);
    }


}

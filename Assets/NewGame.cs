using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{
    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(0);

    }

    public void Adventure()
    {
        SceneManager.LoadScene(3);
    }

    public void Endless()
    {
        SceneManager.LoadScene(6);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using UnityEngine.UI;

public class PlayerLogin : MonoBehaviour
{
    public LeaderBoard board;
    public InputField playerNameInputField;
    public Canvas loginCanvas;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SetUpRoutine());
    }

    public void SetPlayerName()
    {
        
        LootLockerSDKManager.SetPlayerName(playerNameInputField.text, response =>
        {
            if (response.success)
            {
                Debug.Log("Player name set successfully");
            }
            else
            {
                Debug.LogError("Error setting player name: " + response.Error);
            }
        });

        loginCanvas.enabled = false;

        


    }

    IEnumerator SetUpRoutine()
    {
        yield return LoginRoutine();
        yield return board.TopHighScores();


    }

    IEnumerator LoginRoutine()
    {
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("Player Was Logged in");
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                done = true;
            }
            else
            {
                Debug.Log("No session");
                done = true;
            }
        }
        );
        yield return new WaitWhile(() => done == false);

    }
    void Update()
    {
        
    }
}

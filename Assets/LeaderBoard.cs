using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour
{
    int leaderBoardID = 14203;
    public Text PlayerNames;
    public Text PlayerScores;
    // Start is called before the first frame update
    void Start()
    {
        
    }

public IEnumerator SumbitScoreRoutine(int scoreToUpload) {

    bool done = false;
    string playerID = PlayerPrefs.GetString("PlayerID");
    LootLockerSDKManager.SubmitScore(playerID, scoreToUpload, leaderBoardID, (response) =>
     {
        if(response.success)
        {
            Debug.Log("Succes");
            done = true;
        }
        else
        {
            Debug.Log("Failed " + response.Error);
            done = true;
        }
    });
        yield return new WaitWhile(() => done == false);

    }

public IEnumerator TopHighScores()
    {
        bool done = false;
        LootLockerSDKManager.GetScoreListMain(leaderBoardID, 5, 0, (response) =>
        {

            if (response.success)
            {
                string tempPlayerNames = "Names\n";
                string tempPLayerScores = "Scores\n";

                LootLockerLeaderboardMember[] members = response.items;

                for (int i = 0; i < members.Length; i++)
                {
                    tempPlayerNames += members[i].rank + ". ";
                    if (!string.IsNullOrEmpty(members[i].player.name))
                    {
                        tempPlayerNames += members[i].player.name;
                    }
                    else
                    {
                        tempPlayerNames += members[i].player.id;
                    }


                    tempPLayerScores += members[i].score + "\n";
                    tempPlayerNames += "\n";
                }

                done = true;
                PlayerNames.text = tempPlayerNames; 
                PlayerScores.text = tempPLayerScores;
            }
            else
            {
                Debug.Log("Failed" + response.Error);
            }
        });
        yield return new WaitWhile(() => done == false);


    }
}

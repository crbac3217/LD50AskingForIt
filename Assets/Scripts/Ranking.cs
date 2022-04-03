using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Ranking : MonoBehaviour
{
    public GameObject panel1, panel2, singlePrefab, yourScore;
    // Start is called before the first frame update
    void Start()
    {
        GetLeaderBoard();
        ShowYourScore();
    }
    private void GetLeaderBoard()
    {
        var request = new GetLeaderboardRequest { StartPosition = 0, StatisticName = "score", MaxResultsCount = 20 };
        PlayFabClientAPI.GetLeaderboard(request, OnSuccess, OnFailure);
    }
    private void OnSuccess(GetLeaderboardResult result)
    {
        for (int i = 0; i < result.Leaderboard.Count; i++)
        {
            Transform parent = panel1.transform;
            if (i > 9)
            {
                parent = panel2.transform;
            }
            var panel = Instantiate(singlePrefab, parent);
            float val = result.Leaderboard[i].StatValue / 100f;
            panel.GetComponentInChildren<TextMeshProUGUI>().text = result.Leaderboard[i].DisplayName + " : " + val;
        }
    }
    private void ShowYourScore()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            yourScore.GetComponentInChildren<TextMeshProUGUI>().text = PlayerPrefs.GetString("PlayerName") + " : " + PlayerPrefs.GetFloat("HighScore");
        }
        else
        {
            yourScore.GetComponentInChildren<TextMeshProUGUI>().text = "YOU DO NOT HAVE A HIGHSCORE YET";
        }
    }
    private void OnFailure(PlayFabError error)
    {
        Debug.Log("something's wrongo");
    }
    public void Menu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}

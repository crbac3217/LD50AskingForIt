using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine.SceneManagement;

public class InfiniteManager : GameManager
{
    public float score;
    public bool isHighScore;

    public override void Begin()
    {
        foreach (float f in triggerPoints)
        {
            triggers.Add(f);
        }
        blocks = 0;
        startTime = 0;
        timeLeft = 0;
        SpawnSperm();
        IsPlaying = true;
    }
    public override IEnumerator GameOver()
    {
        IsPlaying = false;
        float tempscore = timeLeft + blocks;
        score = Mathf.Round(tempscore * 100f) / 100f;
        isHighScore = CheckHighScore();
        if (isHighScore)
        {
            PlayfabHighScore();
        }
        tmp.gameObject.SetActive(false);
        foreach (Sperm s in sperms)
        {
            s.rb.bodyType = RigidbodyType2D.Static;
            s.enabled = false;
        }
        rotator.GetComponent<BarParent>().enabled = false;
        DepthOfField d;
        volume.profile.TryGet<DepthOfField>(out d);
        while (d.focusDistance.value > 0.1f)
        {
            c.orthographicSize -= 0.005f;
            d.focusDistance.value -= 0.1f;
            yield return null;
        }
        gameOverPanel.SetActive(true);
    }
    private void PlayfabHighScore()
    {
        float temp = score * 100;
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate{StatisticName = "score", Value = (int)temp},
            }
        },
        result => { },
        error => { Debug.LogError(error.GenerateErrorReport());
        });
    }
    public bool CheckHighScore()
    {
        if (!PlayerPrefs.HasKey("HighScore"))
        {
            PlayerPrefs.SetFloat("HighScore", score);
            return true;
        }
        else
        {
            if (PlayerPrefs.GetFloat("HighScore") >= score)
            {
                return false;
            }
            else
            {
                PlayerPrefs.SetFloat("HighScore", score);
                return true;
            }
        }
        
    }
    public override void Game()
    {
        if (IsPlaying)
        {
            timeLeft += Time.deltaTime;
            float tempscore = timeLeft + blocks;
            tmp.text = tempscore.ToString("F2");
            if (triggers.Count > 0)
            {
                if (timeLeft > triggers[0])
                {
                    SpawnSperm();
                    triggers.Remove(triggers[0]);
                }
            }
        }
    }
}

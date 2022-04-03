using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    public InfiniteManager im;
    public GameObject hs;

    private void Start()
    {
        GetComponent<TextMeshProUGUI>().text = "SCORE : " + im.score.ToString("F2");
        if (im.isHighScore)
        {
            hs.gameObject.SetActive(true);
        }
        else
        {
            hs.gameObject.SetActive(false);
        }
    }
}

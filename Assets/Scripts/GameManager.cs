using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI tmp;
    public Vector2[] spawnPoints = new Vector2[4] { Vector2.zero, Vector2.zero, Vector2.zero, Vector2.zero };
    public float[] triggerPoints = new float[] { };
    public GameObject egg, rotator, spermPrefab, gameOverPanel, winPanel, pausePanel;
    public Volume volume;
    public float startTime;
    public int blocks;
    public Camera c;
    public List<Sperm> sperms = new List<Sperm>();
    public bool IsPlaying;
    public float timeLeft;
    public int rotationCount;
    public List<float> triggers = new List<float>();

    private void Start()
    {
        Begin();
    }
    public virtual void Begin()
    {
        foreach (float f in triggerPoints)
        {
            triggers.Add(f);
        }
        blocks = 0;
        timeLeft = startTime;
        SpawnSperm();
        IsPlaying = true;
    }
    #region game
    
    public void Blocked()
    {
        blocks++;
    }
    public virtual IEnumerator GameOver()
    {
        IsPlaying = false;
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
    private IEnumerator Win()
    {
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
        PlayerPrefs.SetInt("BeatMain", 1);
        winPanel.SetActive(true);
    }
    public void SpawnSperm()
    {
        rotationCount++;
        if (rotationCount >= 4)
        {
            rotationCount = 0;
        }
        var sperm = Instantiate(spermPrefab, spawnPoints[rotationCount], Quaternion.identity);
        sperm.GetComponent<Sperm>().egg = egg;
        sperm.GetComponent<Sperm>().gm = this;
        sperms.Add(sperm.GetComponent<Sperm>());
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        rotator.GetComponent<BarParent>().enabled = false;
        DepthOfField d;
        volume.profile.TryGet<DepthOfField>(out d);
        d.focusDistance.value = 0.1f;
        pausePanel.SetActive(true);
        tmp.gameObject.SetActive(false);
    }
    public void ResumeGame()
    {
        rotator.GetComponent<BarParent>().enabled = true;
        rotator.GetComponent<BarParent>().SetUpStat();
        DepthOfField d;
        volume.profile.TryGet<DepthOfField>(out d);
        d.focusDistance.value = 10f;
        pausePanel.SetActive(false);
        tmp.gameObject.SetActive(true);
        Time.timeScale = 1;
    }
    private void Update()
    {
        Game();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }
    public virtual void Game()
    {
        if (IsPlaying)
        {
            timeLeft -= Time.deltaTime;
            tmp.text = timeLeft.ToString("F2");
            if (timeLeft <= 0)
            {
                IsPlaying = false;
                tmp.gameObject.SetActive(false);
                StartCoroutine(Win());
            }
            if (triggers.Count > 0)
            {
                if (timeLeft < triggers[0])
                {
                    SpawnSperm();
                    triggers.Remove(triggers[0]);
                }
            }
        }
    }
    #endregion game
    #region SceneManagement
    public void Retry()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void Menu()
    {
        SceneManager.LoadSceneAsync(0);
    }
    public void Infinite()
    {
        SceneManager.LoadSceneAsync(2);
    }
    #endregion SceneManagement
}

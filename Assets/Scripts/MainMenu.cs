using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class MainMenu : MonoBehaviour
{
    private string[] condom, birthcontrol;
    private int indexCondom, indexBirthControl;
    public Button[] buttonList = new Button[] { };
    public GameObject startPanel, optionPanel;
    private void Start()
    {
        condom = new string[] {"c", "o", "n", "d", "o", "m"};
        birthcontrol = new string[] { "b", "i", "r", "t", "h", "c", "o", "n", "t", "r", "o", "l" };
        PlayfabLogin();
        if (PlayerPrefs.HasKey("PlayerName"))
        {
            ReLoginStartSequence();
        }
        else
        {
            CleanSlate();
        }
    }
    private void ReLoginStartSequence()
    {
        if (PlayerPrefs.GetInt("BeatMain") >= 1)
        {
            buttonList[0].gameObject.SetActive(false);
            buttonList[1].gameObject.SetActive(true);
            buttonList[2].gameObject.SetActive(true);
        }
    }
    private void CleanSlate()
    {
        PlayerPrefs.SetInt("MouseMode", 0);
        PlayerPrefs.SetFloat("WheelSensitivity", 25f);
        PlayerPrefs.SetFloat("MouseSensitivity", 0.5f);
        PlayerPrefs.SetInt("BeatMain", 0);
        startPanel.gameObject.SetActive(true);
    }
    private void PlayfabLogin()
    {
        var request = new LoginWithCustomIDRequest { CustomId = "customid", CreateAccount = true };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSucess, OnLoginFailure);
    }
    private void OnLoginSucess(LoginResult result)
    {

    }
    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogError("LoginError");
        Application.Quit();
    }
    public void LoadScene(int i)
    {
        SceneManager.LoadSceneAsync(i);
    }
    public void Options()
    {
        optionPanel.SetActive(true);
    }
    public void OptionsClose()
    {
        optionPanel.SetActive(false);
    }
    public void QuitApp()
    {
        Application.Quit();
    }
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(condom[indexCondom]))
            {
                indexCondom++;
            }
            else
            {
                indexCondom = 0;
            }
            if (Input.GetKeyDown(birthcontrol[indexBirthControl]))
            {
                indexBirthControl++;
            }
            else
            {
                indexBirthControl = 0;
            }
        }
        if (indexCondom == condom.Length)
        {
            indexCondom = 0;
            buttonList[4].gameObject.SetActive(true);
        }
        if (indexBirthControl == birthcontrol.Length)
        {
            indexBirthControl = 0;
            buttonList[5].gameObject.SetActive(true);
        }
    }
}

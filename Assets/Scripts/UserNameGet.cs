using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class UserNameGet : MonoBehaviour
{
    public ButtonSound butt;
    public string userName;
    public GameObject success, fail;
    // Start is called before the first frame update
    public void inputChanged(string st)
    {
        userName = st;
        if (!butt)
        {
            Debug.Log("we need butt here");
        }
    }
    public void CheckUserName()
    {
        butt.OnButtonPress();
        var request = new UpdateUserTitleDisplayNameRequest { DisplayName = userName };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, UsernameSuccess, UsernameFail);
    }
    private void UsernameSuccess(UpdateUserTitleDisplayNameResult result)
    {
        PlayerPrefs.SetString("PlayerName", userName);
        success.SetActive(true);
    }
    private void UsernameFail(PlayFabError error)
    {
        fail.SetActive(true);
    }
    public void ClosePanel()
    {
        butt.OnButtonPress();
        this.gameObject.SetActive(false);
    }
    public void CloseFailPanel()
    {
        butt.OnButtonPress();
        fail.SetActive(false);
    }
}

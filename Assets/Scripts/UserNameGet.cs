using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class UserNameGet : MonoBehaviour
{
    public string userName;
    public GameObject success, fail;
    // Start is called before the first frame update
    public void inputChanged(string st)
    {
        userName = st;
    }
    public void CheckUserName()
    {
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
        this.gameObject.SetActive(false);
    }
    public void CloseFailPanel()
    {
        fail.SetActive(false);
    }
}

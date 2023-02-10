using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject TitleObject;
    public GameObject UIMenuObject;
    public GameObject CreditsPanel;

    void Update()
    {
        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Escape) && CreditsPanel.activeSelf)
        {
            CloseCredits();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void CloseCredits()
    {
        TitleObject.SetActive(true);
        UIMenuObject.SetActive(true);
        CreditsPanel.SetActive(false);
    }

    public void ShowCredits()
    {
        TitleObject.SetActive(false);
        UIMenuObject.SetActive(false);
        CreditsPanel.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

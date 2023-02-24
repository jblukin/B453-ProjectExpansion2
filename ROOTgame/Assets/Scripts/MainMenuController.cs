using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject TitleObject;
    public GameObject UIMenuObject;
    public GameObject CreditsPanel;
    [SerializeField] private GameObject LevelSelectPanel;
    [SerializeField] private GameObject LevelSelectText;

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
        LevelSelectPanel.SetActive(false);
        LevelSelectText.SetActive(false);
    }

    public void ShowCredits()
    {
        TitleObject.SetActive(false);
        UIMenuObject.SetActive(false);
        CreditsPanel.SetActive(true);
        LevelSelectPanel.SetActive(false);
        LevelSelectText.SetActive(false);
    }

    public void ShowLevelSelectPanel()
    {
        TitleObject.SetActive(false);
        UIMenuObject.SetActive(false);
        CreditsPanel.SetActive(false);
        LevelSelectPanel.SetActive(true);
        LevelSelectText.SetActive(true);
    }

    public void CloseLevelSelectPanel()
    {
        TitleObject.SetActive(true);
        UIMenuObject.SetActive(true);
        CreditsPanel.SetActive(false);
        LevelSelectPanel.SetActive(false);
        LevelSelectText.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

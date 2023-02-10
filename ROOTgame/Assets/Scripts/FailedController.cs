using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FailedController : MonoBehaviour
{
    public void RetryLevel()
    {
        SceneManager.LoadScene("Level_" + PlayerPrefs.GetInt("currLevel"));
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

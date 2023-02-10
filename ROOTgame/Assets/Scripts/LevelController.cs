using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public int currentLevel = 1;
    int maxLevel = 8;
    public GameObject backgroundObject;
    public Sprite[] spriteList;
    public GameObject doorObject;
    public int numberOfFiles;
    int currentNumberOfFiles = 0;
    public AudioSource crunchSound;
    public PlayerController playerController;
    bool pauseCondition = false;
    public GameObject pausePanel;
    // Start is called before the first frame update
    void Start()
    {
        backgroundObject.GetComponent<SpriteRenderer>().sprite = spriteList[Random.Range(0, spriteList.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Start"))
        {
            TogglePauseGame();
        }
        if (currentNumberOfFiles >= numberOfFiles)
        {
            openDoor();
        }
    }

    public void TogglePauseGame()
    {
        pauseCondition = !pauseCondition;
        if (pauseCondition)
        {
            Time.timeScale = 0f;
        }
        else if (!pauseCondition)
        {
            Time.timeScale = 1f;
        }
        pausePanel.SetActive(pauseCondition);
    }

    public void openDoor()
    {
        doorObject.GetComponent<Animator>().SetBool("open", true);
    }

    public void ReceiveFile()
    {
        crunchSound.Play();
        currentNumberOfFiles++;
    }

    public void NextStage()
    {
        if (currentLevel + 1 <= maxLevel)
        {
            SceneManager.LoadScene("Level_" + (currentLevel + 1));
        }
        else
        {
            SceneManager.LoadScene("Success");
        }
    }

    public void DeadgeScreen()
    {
        PlayerPrefs.SetInt("currLevel", currentLevel);
        SceneManager.LoadScene("Failed");
    }

    public void BackToMenu()
    {
        pauseCondition = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}

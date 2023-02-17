using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public int currentLevel = 1;
    int maxLevel = 9;
    public GameObject backgroundObject;
    public Sprite[] spriteList;
    public GameObject doorObject;
    int numberOfFiles;
    int currentNumberOfFiles = 0;
    bool turretPresent = false;
    [System.NonSerialized] public int enemiesRemaining;
    Collider2D[] objectsInLevel;
    public AudioSource crunchSound;
    public PlayerController playerController;
    bool pauseCondition = false;
    public GameObject pausePanel;
    // Start is called before the first frame update
    void Start()
    {

        enemiesRemaining = -1;

        numberOfFiles = 0;

        backgroundObject.GetComponent<SpriteRenderer>().sprite = spriteList[Random.Range(0, spriteList.Length)];

        objectsInLevel = Physics2D.OverlapCircleAll(Vector2.zero, 100f);

        foreach (Collider2D obj in objectsInLevel) {

            if(obj.gameObject.CompareTag("Enemy")) {

                enemiesRemaining++;

            }

            if(obj.gameObject.CompareTag("Turret")) {

                turretPresent = true;

            }

            if(obj.gameObject.CompareTag("File")) {

                numberOfFiles++;

            }

        }

        currentNumberOfFiles = 0;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Start"))
        {
            TogglePauseGame();
        }
        if (!turretPresent)
        {

            if(currentNumberOfFiles >= numberOfFiles)
                openDoor();

        } else {

            // Debug.Log("Files: " + currentNumberOfFiles);

            // Debug.Log("Enemies " + enemiesRemaining);

            if((currentNumberOfFiles >= numberOfFiles) && (enemiesRemaining == 0))
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveFilesController : MonoBehaviour
{
    public Sprite[] spriteList;
    LevelController levelController;
    void Start()
    {
        levelController = GameObject.Find("Level Controller").GetComponent<LevelController>();
        GetComponent<SpriteRenderer>().sprite = spriteList[Random.Range(0, spriteList.Length)];
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            levelController.ReceiveFile();
            Destroy(gameObject);
        }
    }
}

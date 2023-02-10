using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    AIPath aiPath;
    void Start()
    {
        aiPath = GetComponent<AIPath>();
        GetComponent<AIDestinationSetter>().target = GameObject.Find("Player").transform;
    }
    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("collision enter: " + collision.gameObject.name);
    //}

    //void OnCollisionStay2D(Collision2D collision)
    //{
    //    Debug.Log("collision stay: " + collision.gameObject.name);
    //}

    //void OnCollisionExit2D(Collision2D collision)
    //{
    //    Debug.Log("collision exit: " + collision.gameObject.name);
    //}

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("trigger enter: " + collision.gameObject.name);S
        if (collision.CompareTag("Stun"))
        {
            aiPath.canMove = false;
            Invoke("MoveAgain", 1f);
        }
    }

    //void OnTriggerStay2D(Collider2D collision)
    //{
    //    Debug.Log("trigger stay: " + collision.gameObject.name);
    //}

    //void OnTriggerExit2D(Collider2D collision)
    //{
    //    Debug.Log("trigger exit: " + collision.gameObject.name);
    //}

    void MoveAgain()
    {
        aiPath.canMove = true;
    }
}

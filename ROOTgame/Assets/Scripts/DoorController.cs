using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public void DisableDoorCollider()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMovement_Script : MonoBehaviour
{
    public bool movingForward;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.tag == "Collectable" || collision.tag == "Potion")
        {
            return;
        }

        if (collision.tag == "Player")
        {
            PlayerController_Script.staticPlayer.Kill();
        }

        if (movingForward == true)
        {
            Enemy_Script.turnAround = true;
        }
        else
        {
            Enemy_Script.turnAround = false;
        }

        movingForward = !movingForward;
    }
}

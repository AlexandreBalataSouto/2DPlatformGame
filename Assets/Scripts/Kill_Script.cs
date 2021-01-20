using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill_Script : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerController_Script.staticPlayer.Kill(); //Llamamos a la funcion Kill() que esta en PlayerController_Script
        }
    }
}

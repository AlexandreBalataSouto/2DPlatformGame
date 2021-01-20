using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Script : MonoBehaviour
{
    public float runningSeepd = 5f;

    private Rigidbody2D rigidbody;

    public static bool turnAround;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float currentRunningSpeed = runningSeepd;

        if(turnAround == true)
        {
            currentRunningSpeed = runningSeepd;
            this.transform.eulerAngles = new Vector3(0, 180.0f, 0); //Hacemos que el enemigo se de la vuelta 
        }
        else
        {
            currentRunningSpeed = -runningSeepd;
            this.transform.eulerAngles = new Vector3(0, 0, 0);
        }

        if(GameManager_Script.staticGameManager.currentGameState == GameState.inGame)
        {
            rigidbody.velocity = new Vector2(currentRunningSpeed, rigidbody.velocity.y);

        }

        if(GameManager_Script.staticGameManager.currentGameState == GameState.gameover || GameManager_Script.staticGameManager.currentGameState == GameState.menu)
        {
            rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
        }
    }
}

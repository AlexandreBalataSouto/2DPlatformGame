using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewInGame_Script : MonoBehaviour
{
    public Text collectableLabel;
    public Text scoreLabel;
    public Text maxScoreLabel;

    void Update()
    {
        if(GameManager_Script.staticGameManager.currentGameState == GameState.inGame || GameManager_Script.staticGameManager.currentGameState == GameState.gameover)
        {
            int currentObjects = GameManager_Script.staticGameManager.collectedObjects;
            this.collectableLabel.text = currentObjects.ToString();
        }

        if(GameManager_Script.staticGameManager.currentGameState == GameState.inGame)
        {
            float distance = PlayerController_Script.staticPlayer.GetDistance();
            this.scoreLabel.text = "Score\n"+distance.ToString("f1"); //f1 para decir que muestre un decimal

            float maxscore = PlayerPrefs.GetFloat("maxscore", 0);
            this.maxScoreLabel.text = "MaxScore\n" + maxscore.ToString("f1");

        }
    }
}

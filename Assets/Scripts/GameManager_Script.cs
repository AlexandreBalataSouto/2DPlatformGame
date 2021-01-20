using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState //Esto es un enumerado o falsa clase
{//Posibles estados del juego
    menu,
    inGame,
    gameover
}

public class GameManager_Script : MonoBehaviour
{
    public static GameManager_Script staticGameManager; //Variable que referencia al propio Game Manager (variable global)

    public GameState currentGameState = GameState.menu;  //Variable para saber en que estado del juego nos encontramos

    public Canvas menuCanvas; //Variable para el menu principal
    public Canvas inGameCanvas; //Variable para el menu del juego
    public Canvas gameOverCanvas; //Variable para el game over

    public int collectedObjects = 0;

    private void Awake()
    {
        staticGameManager = this; //Declaramos la variable para que sea un sigleton
    }

    private void Start()
    {
        BackToMenu();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Start") && currentGameState != GameState.inGame)
        {
           
            StartGame();

        }

        if(Input.GetButtonDown("Pause"))
        {
            BackToMenu();
            PlayerController_Script.staticPlayer.StopCoroutine("TirePlayer"); //Paramos la corutina cuando hacemos pausa
        }

        if (Input.GetKeyDown(KeyCode.Escape)) //Si pultamos la tecla escape
        {
            ExitGame();
        }
    }


    //Funciones de estado del juego
    public void StartGame()
    {
        SetGameState(GameState.inGame);
        
        CameraFollow_Script.staticCameraFollow.ResetCameraPosition(); //La camara vuelve a la posicion de inicio

        if(PlayerController_Script.staticPlayer.transform.position.x > 0) //Aqui cumpliendo la condicion generamos el nivel desde el principio
        {
            LevelGenerator_Script.staticLevelGenerator.RemoveAllLevelBlocks();
            LevelGenerator_Script.staticLevelGenerator.GenerateInitialLevelBlocks();
        }

        PlayerController_Script.staticPlayer.StartPlayerGame(); //La funcion provoca que el jugador vuelva a su posicion original

        collectedObjects = 0;
    }//END StartGame

    public void ExitGame()
    {
        //Application.Quit(); Esto solo funciona en el juego final o version de produccion, es decir, no funciona en el editor 
        //Sin embargo existe una forma de probarlo

        /**
         * Sucede que dependiendo de la plataforma puede haber cosas que funcionen y otras no
         * dada la misma naturaleza del dispositivo, para este caso existen estos "#if" que 
         * nos permiten usarlos de forma "generica" independientemente del dispositivo.
         * 
         * Esta pensado para hacer multiplataforma 
         * */

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit(); 
        #endif

        //Ahora si podemos hacer  Application.Quit() en el editor

    }

    public void GameOver()
    {
        SetGameState(GameState.gameover);
    }
  
    public void BackToMenu()
    {
        SetGameState(GameState.menu);
    }
    //END funciones estado del juego


    void SetGameState(GameState newGameState) //metodo encargado de cambiar el estado del juego
    {

        if(newGameState == GameState.menu) //PROBAR CON UN SWITCH MEJOR
        {
            //MENU
            menuCanvas.enabled = true; //Activamos el menú
            inGameCanvas.enabled = false;
            gameOverCanvas.enabled = false;

        } else if(newGameState == GameState.inGame)
        {
            //INGAME
            menuCanvas.enabled = false; //Activamos el menú
            inGameCanvas.enabled = true;
            gameOverCanvas.enabled = false;
        }
        else if(newGameState == GameState.gameover)
        {
            //GAMEOVER
            menuCanvas.enabled = false;
            inGameCanvas.enabled = false;
            gameOverCanvas.enabled = true;
        }

        this.currentGameState = newGameState; //Cambio de estado
    } //END SetGameState

    public void CollectObject(int objectValue) //Metodo para sumar puntacion
    {
        this.collectedObjects += objectValue;

    }


}

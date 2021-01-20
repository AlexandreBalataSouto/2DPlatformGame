using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Script : MonoBehaviour
{

    public static PlayerController_Script staticPlayer; //Variable que referencia al propio Player (variable global) concepto "sigleton"

    public float jumpForce = 60f;
    public float runningSeepd = 5f;

    public Animator animator; //Varaible para obtener la animacion (Referencia a la animacion)

    private Rigidbody2D rigidbody; //Necesitamos el rigidbody para aplicarle la fuerza del salto

    private Vector3 startPosition; //Variable para guardar la posicion primera del personaje

    private int healthPoint, manaPoint; //Vida y mana

    public const int INITIAL_HEALTH = 100;
    public const int INITIAL_MANA = 10;
    public const int MAX_HEALTH = 100;
    public const int MAX_MANA = 10;
    public const int MIN_HEALTH = 60;

    public const float MIN_SPEED = 0.05f;
    public const float HEALTH_TIME_DECREASE = 1.0f;

    public const int SUPERJUMP_COST = 3;
    public const float SUPERJUMP_FORCE = 1.5F;

    
    void Awake() //LAS CONFIGURACIONES O DECLARACIONES PREVIAS DE VARIABLES SIEMPRE AQUI PRIMERO
    {
        staticPlayer = this; //Declaramos la variable para que sea un sigleton
        rigidbody = GetComponent<Rigidbody2D>();
        startPosition = this.transform.position; //Posicion actual del personaje
    }


    public void StartPlayerGame() 
    {
        animator.SetBool("isAlive", true); //Establecemos como true los Parametros del Animator
        animator.SetBool("isGrounded", true);
        this.transform.position = startPosition; //Al empezar, devolvemos al personaje a su posicion original 

        this.healthPoint = INITIAL_HEALTH;
        this.manaPoint = INITIAL_MANA;

        StartCoroutine("TirePlayer"); //Empieza la corutina
    }

    IEnumerator TirePlayer()
    {
        while(this.healthPoint > MIN_HEALTH)
        {
            this.healthPoint--;
            yield return new WaitForSeconds(HEALTH_TIME_DECREASE);
        }

        yield return null;
    }

    void Update()
    {
        if(GameManager_Script.staticGameManager.currentGameState == GameState.inGame) //Llamamos a la variable global para ver si el jugador esta jugando (inGame)
        {
            if (Input.GetMouseButtonDown(0)) //Si pulsamos la tecla espacio o el si hacemos click con le raton
            {
                Jump(false);
            }

            if (Input.GetMouseButtonDown(1)) //Si pulsamos la tecla espacio o el si hacemos click con le raton
            {
                Jump(true);
            }

            animator.SetBool("isGrounded", IsTouchingTheGound()); //Establecemos como false los Parametros del Animator porque el personaje ya NO toca el suelo
                                                                  //Aprovechamos el metodo isTouchingTheGround
        }

    }

     void FixedUpdate() //A la hora de aplicar fuerzas constantes usamos este metodo ya que los frames son constantes mientras que en Update pueden variar
    {
        if(GameManager_Script.staticGameManager.currentGameState == GameState.inGame) //Llamamos a la variable global para ver si el jugador esta jugando (inGame)
        {
            float currentSpeed = (runningSeepd - MIN_SPEED) * this.healthPoint / 100.0f;
            if (rigidbody.velocity.x < currentSpeed)
            {                          //Vector2(velocidadEjeX,       velocidadEjeY)         
                rigidbody.velocity = new Vector2(currentSpeed, rigidbody.velocity.y);
                //La velocidad vertical que tiene en ese momento
            }
        }
    }

    void Jump(bool isSuperJump)
    {
        //F = m * a
        //Fuerza = masa * aceleracion
        if (IsTouchingTheGound()) //Si esta tocando el suelo
        {
            if(isSuperJump && this.manaPoint >= SUPERJUMP_COST)
            {
                manaPoint -= SUPERJUMP_COST;
                rigidbody.AddForce(Vector2.up * jumpForce * SUPERJUMP_FORCE, ForceMode2D.Impulse);
            }
            else
            {
                rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); //Fuerza vertical (Vector2.up) * fuerza del salto, ademas indicamos que la fuerza es un impulso
            }
        }
       
    }

    public LayerMask groundLayer; //variable para detectar la capa del suelo

    bool IsTouchingTheGound() //Metodo para comprobar si esta tocando el suelo
    {
        //Trazamos un rayo imaginario desde la posicion del jugador //hacia abajo // hasta un maximo de 20 cm// hasta la capa del suelo
        if (Physics2D.Raycast(this.transform.position,Vector2.down, 0.2f, groundLayer)){

            return true;
        }
        else
        {
            return false;
        }
    }

    public void Kill() 
    {
        GameManager_Script.staticGameManager.GameOver(); 
        this.animator.SetBool("isAlive", false); // Cambiamos el valor booleano para que se active la animacion de muerte

        float currentMaxScore = PlayerPrefs.GetFloat("maxscore", 0); //PlayerPrefs para guardar la puntuacion del jugador

        if(currentMaxScore < this.GetDistance())
        {
            PlayerPrefs.SetFloat("maxscore", this.GetDistance());
        }

        StopCoroutine("TirePlayer");
    }

    public float GetDistance() //Nos da la distancia que ha recorrido el jugador
    {
        float distance = Vector2.Distance(new Vector2(startPosition.x,0) , new Vector2(this.transform.position.x,0));

        return distance;
    }

    public void CollectHeatlh(int points)
    {
        this.healthPoint += points;

        if(this.healthPoint >= MAX_HEALTH)
        {
            this.healthPoint = MAX_HEALTH;
        }
    }

    public void CollectMana(int points)
    {
        this.manaPoint += points;

        if(this.manaPoint >= MAX_MANA)
        {
            this.manaPoint = MAX_MANA;
        }
    }

    public int GetHealth()
    {
        return this.healthPoint;
    }

    public int GetMana()
    {
        return this.manaPoint;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Rock")
        {
            this.healthPoint -= 10;
        }
    }
}

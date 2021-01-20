using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow_Script : MonoBehaviour
{
    public static CameraFollow_Script staticCameraFollow; //sigleton

    public Transform target; //Variable para saber a quien sigue la camara

    public Vector3 offset = new Vector3(0.1f, 0.0f, -10f); //Variable para determinar como de lejos va seguir al jugador

    public float dampTime = 0.3f; //Variable para hacer que la camara este quieta un lapsode tiempo y despues comience a seguir al jugador

    public Vector3 velocity = Vector3.zero; //Velocidad de la camara

    private void Awake()
    {
        Application.targetFrameRate = 60; //Renderiza, si puedes, a 60 frames

        staticCameraFollow = this;
    }


    public void ResetCameraPosition()
    {
        //Coordenadas del mundo a coordenadas del personaje
        Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);

        //Coordenadas del personaje a coordenadas del mundo
        Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(offset.x, offset.y, point.z)); //Correcion para la camara

        Vector3 destination = point + delta;

        destination = new Vector3(target.position.x, offset.y, offset.z); //Esto evita que si el personaje salta la camara le siga 

        this.transform.position = destination;
    }


    void Update()
    {
                                             //Coordenadas del mundo a coordenadas del personaje
        Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);

                                     //Coordenadas del personaje a coordenadas del mundo
        Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(offset.x, offset.y, point.z)); //Correcion para la camara

        Vector3 destination = point + delta;

        destination = new Vector3(target.position.x, offset.y, offset.z); //Esto evita que si el personaje salta la camara le siga 

        this.transform.position = Vector3.SmoothDamp(this.transform.position, destination, ref velocity, dampTime); //SmoothDamp hace que la camara se mueva suavemente SUAVE
    }
}

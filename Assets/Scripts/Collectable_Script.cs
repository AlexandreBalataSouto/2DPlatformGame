using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CollectableType
{
    healthPotion,
    manaPotion,
    money
}

public class Collectable_Script : MonoBehaviour
{
    public CollectableType type = CollectableType.money;

    bool isCollected = false;
    public int value = 0;

    public AudioClip collectSound;

    void Show()
    {
        this.GetComponent<SpriteRenderer>().enabled = true; //Activamos la imagen del coleccionable
        this.GetComponent<CircleCollider2D>().enabled = true; //Activamos el collider
        isCollected = false;
    }

    void Hide()
    {
        this.GetComponent<SpriteRenderer>().enabled = false; 
        this.GetComponent<CircleCollider2D>().enabled = false; 
    }

    void Collect()
    {
        isCollected = true;
        Hide();

        AudioSource audio = GetComponent<AudioSource>(); //Get Audio Source del inspector

        if(audio != null && this.collectSound != null)
        {
            audio.PlayOneShot(this.collectSound); //Reproduce el sonido
        }

        switch (this.type)
        {
            case CollectableType.money:
                GameManager_Script.staticGameManager.CollectObject(value);
                break;
            case CollectableType.healthPotion:
                PlayerController_Script.staticPlayer.CollectHeatlh(value);
                break;
            case CollectableType.manaPotion:
                PlayerController_Script.staticPlayer.CollectMana(value);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.tag == "Player")
        {
            Collect();
        }
       
    }
}

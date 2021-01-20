using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BarType
{
    health,
    mana
}

public class PlayerBarController_Script : MonoBehaviour
{

    private Slider slider;
    public BarType type;
    
    void Start()
    {
        this.slider = GetComponent<Slider>();

        switch (this.type)
        {
            case BarType.health:
                this.slider.maxValue = PlayerController_Script.MAX_HEALTH;
                break;
            case BarType.mana:
                this.slider.value = PlayerController_Script.MAX_MANA;
                break;
        }
    }

    void Update()
    {
        switch (this.type)
        {
            case BarType.health:
                this.slider.value = PlayerController_Script.staticPlayer.GetHealth();
                break;
            case BarType.mana:
                this.slider.value = PlayerController_Script.staticPlayer.GetMana();
                break;
        }
    }
}

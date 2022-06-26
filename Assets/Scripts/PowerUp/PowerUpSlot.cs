using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerUpSlot : MonoBehaviour
{
    public Button button;
    public Image border;
    public TMP_Text title;
    public Image icon;
    public TMP_Text description;

    public PowerUpSO powerUp;
    private void Start()
    {
        //button.onClick.AddListener(() => { ChooseThisPowerUp(); });
    }

    public void OnMouseHover()
    {
        border.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

    public void OnMouseLeave()
    {
        border.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    }

    void ChooseThisPowerUp()
    {
        if(powerUp.replaceAbility)
        {
            switch(powerUp.abilityType)
            {
                case AbilityType.dashAbility:
                    PowerUpManager.Instance.player.GetComponent<Player>().dashAbility = powerUp.ability;
                    break;
                case AbilityType.jumpAbility:
                    PowerUpManager.Instance.player.GetComponent<Player>().jumpAbility = powerUp.ability;
                    break;
                case AbilityType.leftClickAbility:
                    PowerUpManager.Instance.player.GetComponent<Player>().leftClickAbility = powerUp.ability;
                    break;
                case AbilityType.rightClickAbility:
                    PowerUpManager.Instance.player.GetComponent<Player>().rightClickAbility = powerUp.ability;
                    break;
            }
        }
        else
        {
            PowerUpManager.Instance.player.GetComponent<Character>().ApplyMod(powerUp.statEnum, powerUp.mod);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class PowerUpManager : Singleton<PowerUpManager>
{
    public CanvasGroup powerUpPanel;
    public List<PowerUpSO> allPowerUp;
    public List<PowerUpSlot> slots;
    public GameObject player;
    public void OpenPanel()
    {
        GetComponent<CanvasGroup>().alpha = 1.0f;
        GetComponent<CanvasGroup>().interactable = true;
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        GeneratePowerUp();
    }

    public void ClosePanel()
    {
        GetComponent<CanvasGroup>().alpha = 0.0f;
        GetComponent<CanvasGroup>().interactable = false;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    [Button]
    void GeneratePowerUp()
    {
        //TODO : multiplayer need to target correct player
        player = GameObject.FindGameObjectWithTag("Player");

        List<PowerUpSO> temp = new List<PowerUpSO>();
        
        foreach(PowerUpSO powerUp in allPowerUp)
        {
            if(powerUp.restrictedTo is null ||
                powerUp.restrictedTo == player.GetComponent<Player>().weapon ||
                powerUp.restrictedTo == player.GetComponent<Player>().armor ||
                powerUp.restrictedTo == player.GetComponent<Player>().accessory) //switch case must be constant value so forced to use long if-else
            {
                temp.Add(powerUp);
            }
        }

        for(int i = 0; i < slots.Count; i++)
        {
            int index = Random.Range(0, temp.Count);
            PowerUpSO powerUp = temp[index];
            temp.RemoveAt(index);

            slots[i].title.text = powerUp.title;
            slots[i].icon.sprite = powerUp.icon;
            slots[i].description.text = powerUp.description;
            slots[i].powerUp = powerUp;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Sirenix.OdinInspector;

public class LoadoutManager : Singleton<LoadoutManager>
{
    public CanvasGroup loadoutPanel;
    public List<EquipmentSO> weaponList;
    public List<EquipmentSO> armorList;
    public List<EquipmentSO> accessoryList;

    [Header("UI")]
    public Button startButton;
    public GameObject tooltip;

    [Header("Weapon")]
    public Image weaponSlot;
    public Image weaponAbility1;
    public Image weaponAbility2;


    [Header("Armor")]
    public Image armorSlot;
    public Image armorAbility1;

    [Header("Accessory")]
    public Image accessorySlot;
    public Image accessoryAbility1;

    [HideInInspector] public int weaponSelection;
    [HideInInspector] public int armorSelection;
    [HideInInspector] public int accessorySelection;

    private void Start()
    {
        startButton.onClick.AddListener(() => { Ready(); });

        weaponSelection = 0;
        armorSelection = 0;
        accessorySelection = 0;
    }

    void Ready()
    {
        //TODO : wait for all player ready, then start game
        SummonCharacter();
    }

    void SummonCharacter()
    {
        loadoutPanel.alpha = 0f;
        loadoutPanel.interactable = false;
        loadoutPanel.blocksRaycasts = false;

        //TODO : multiplayer need to target correct player
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        player.GetComponent<Player>().leftClickAbility = weaponList[weaponSelection].abilities[AbilityType.leftClickAbility];
        player.GetComponent<Player>().rightClickAbility = weaponList[weaponSelection].abilities[AbilityType.rightClickAbility];
        //TODO : for amor and accessory in future we can do for other ability

        player.transform.parent = null;
        player.GetComponent<Rigidbody2D>().simulated = true;
        
        Camera.main.transform.DOMoveY(5f, 2.5f).OnComplete(()=>{
            player.GetComponent<Player>().weaponModel.GetComponent<Weapon>().enabled = true;
            player.GetComponent<PlayerController>().enabled = true;
        });
    }

    public void ChangeSelection(int type)
    {
        switch(type)
        {
            case 0:
                weaponSelection++;
                if (weaponSelection >= weaponList.Count)
                    weaponSelection = 0;

                weaponSlot.sprite = weaponList[weaponSelection].icon;
                weaponAbility1.sprite = weaponList[weaponSelection].abilities[AbilityType.leftClickAbility].icon;
                weaponAbility2.sprite = weaponList[weaponSelection].abilities[AbilityType.rightClickAbility].icon;
                break;
            case 1:
                weaponSelection--;
                if (weaponSelection <= 0)
                    weaponSelection = weaponList.Count - 1;

                weaponSlot.sprite = weaponList[weaponSelection].icon;
                weaponAbility1.sprite = weaponList[weaponSelection].abilities[AbilityType.leftClickAbility].icon;
                weaponAbility2.sprite = weaponList[weaponSelection].abilities[AbilityType.rightClickAbility].icon;
                break;
            case 2:
                armorSelection++;
                if (armorSelection >= armorList.Count)
                    armorSelection = 0;
                armorSlot.sprite = armorList[armorSelection].icon;
                //armorAbility1.sprite = armorList[armorSelection].abilities[AbilityType.xxx].icon;
                break;
            case 3:
                armorSelection--;
                if (armorSelection <= 0)
                    armorSelection = armorList.Count - 1;
                armorSlot.sprite = armorList[armorSelection].icon;
                //armorAbility1.sprite = armorList[armorSelection].abilities[AbilityType.xxx].icon;
                break;
            case 4:
                accessorySelection++;
                if (accessorySelection >= accessoryList.Count)
                    accessorySelection = 0;
                accessorySlot.sprite = accessoryList[accessorySelection].icon;
                //accessoryAbility1.sprite = accessoryList[accessorySelection].abilities[AbilityType.xxx].icon;
                break;
            case 5:
                accessorySelection--;
                if (accessorySelection <= 0)
                    accessorySelection = accessoryList.Count - 1;
                accessorySlot.sprite = accessoryList[accessorySelection].icon;
                //accessoryAbility1.sprite = accessoryList[accessorySelection].abilities[AbilityType.xxx].icon;
                break;
        }
    }

    public void ShowTooltip(string text)
    {
        tooltip.GetComponent<CanvasGroup>().alpha = 1.0f;
        tooltip.GetComponent<CanvasGroup>().blocksRaycasts = true;
        tooltip.GetComponent<CanvasGroup>().interactable = true;

        tooltip.transform.position = Input.mousePosition;
        tooltip.GetComponentInChildren<TMP_Text>().text = text;
    }

    public void HideTooltip()
    {
        tooltip.GetComponent<CanvasGroup>().alpha = 0.0f;
        tooltip.GetComponent<CanvasGroup>().blocksRaycasts = false;
        tooltip.GetComponent<CanvasGroup>().interactable = false;
    }
}

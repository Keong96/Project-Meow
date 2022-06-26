using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "PowerUp", menuName = "ScriptableObjects/PowerUp", order = 1)]
public class PowerUpSO : SerializedScriptableObject
{
    public Sprite icon;
    public string title;
    public string description;

    [Header("Settings")]
    public bool replaceAbility;

    [HideIf("replaceAbility")] public StatEnum statEnum;
    [HideIf("replaceAbility")] public EquipmentSO restrictedTo;
    [HideIf("replaceAbility")] public Modifier mod;

    [ShowIf("replaceAbility")] public AbilityType abilityType;
    [ShowIf("replaceAbility")] public Ability ability;
}

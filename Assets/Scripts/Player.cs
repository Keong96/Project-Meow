using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [Header("Ability")]
    [SerializeReference] public Ability jumpAbility;
    [SerializeReference] public Ability dashAbility;
    [SerializeReference] [ReadOnly] public Ability leftClickAbility;
    [SerializeReference] [ReadOnly] public Ability rightClickAbility;

    [Header("Equipment")]
    [SerializeReference] public EquipmentSO weapon;
    [SerializeReference] public EquipmentSO armor;
    [SerializeReference] public EquipmentSO accessory;
}

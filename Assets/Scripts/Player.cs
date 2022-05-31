using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

public class Player : Character
{
    [Header("Ability")]
    [SerializeReference] public Ability jumpAbility;
    [SerializeReference] public Ability leftClickAbility;
    [SerializeReference] public Ability rightClickAbility;
}

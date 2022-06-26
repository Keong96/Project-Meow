using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "Equipment", menuName = "ScriptableObjects/Equipment", order = 1)]
public class EquipmentSO : SerializedScriptableObject
{
    public Sprite icon;
    public GameObject model;
    public string title;
    public string description;
    public float damage;

    [Header("Ability")]
    public Dictionary<AbilityType, Ability> abilities;
}

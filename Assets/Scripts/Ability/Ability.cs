using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AbilityType
{
    jumpAbility,
    dashAbility,
    leftClickAbility,
    rightClickAbility
}

public class Ability
{
    public Sprite icon;
    public float damage;
    public float cooldown = 0;
    public virtual void Perform(Character caster)
    {

    }

    public virtual void Perform(Character caster, Character target)
    {

    }
}

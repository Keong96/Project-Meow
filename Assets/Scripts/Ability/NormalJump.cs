using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalJump : Ability
{
    public override void Perform(Character caster)
    {
        Rigidbody2D rb = caster.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.up * caster.jumpVelocity.currentValue;
    }
}

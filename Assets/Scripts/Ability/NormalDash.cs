using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NormalDash : Ability
{
    public GameObject dash_VFX_Prefab;
    public override void Perform(Character caster)
    {
        if(caster.numOfDash < 1)
        {
            caster.canMove = false;
            caster.canJump = false;

            Rigidbody2D rb = caster.GetComponent<Rigidbody2D>();
            Collider2D col = caster.GetComponent<Collider2D>();

            col.enabled = false;
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0f;

            GameObject VFX = GameObject.Instantiate(dash_VFX_Prefab, caster.transform.position, Quaternion.identity);
            VFX.transform.localScale = new Vector3(caster.transform.localScale.x, 1f, 1f);
            GameObject.Destroy(VFX, 0.15f);

            float destination = caster.transform.position.x +(caster.transform.localScale.x * caster.dashValue.currentValue);

            caster.GetComponent<Rigidbody2D>().DOMoveX(destination, 0.25f).OnComplete(() => {
                caster.canMove = true;
                caster.canJump = true;
                
                col.enabled = true;
                rb.gravityScale = 1f;
                caster.numOfDash++;
            });
        }
    }
}

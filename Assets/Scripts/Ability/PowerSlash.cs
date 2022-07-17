using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PowerSlash : Ability
{
    public GameObject vfxPrefab;
    public override void Perform(Character caster)
    {
        caster.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        caster.GetComponent<Rigidbody2D>().gravityScale = 0f;
        caster.canMove = false;
        caster.canJump = false;
        caster.canDash = false;

        float rotation = caster.weaponHolder.transform.rotation.z + (caster.transform.localScale.x * -360f);
        caster.weaponHolder.transform.DORotate(new Vector3(0f, 0f, rotation), 0.35f, RotateMode.LocalAxisAdd).OnComplete(()=> {
            caster.GetComponent<Rigidbody2D>().gravityScale = 1f;
            caster.canMove = true;
            caster.canJump = true;
            caster.canDash = true;
        });

        GameObject vfx = GameObject.Instantiate(vfxPrefab, caster.transform.position, Quaternion.identity);
        vfx.transform.localScale = new Vector3(1.0f * caster.transform.localScale.x, 1.0f, 1.0f);
        GameObject.Destroy(vfx, 1f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttack : Ability
{
    public GameObject projectilePrefab;
    public override void Perform(Character caster)
    {
        caster.anim.SetTrigger("Attack");

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = new Vector3(worldPosition.x, worldPosition.y, 0f) - new Vector3(caster.transform.position.x, caster.transform.position.y, 0f);

        GameObject go = GameObject.Instantiate(projectilePrefab, caster.transform.position, Quaternion.identity);
        go.GetComponent<Projectile>().caster = caster;
        go.GetComponent<Projectile>().lifetime = 10f;
        go.GetComponent<Projectile>().speed = 5f;
        go.GetComponent<Projectile>().damage = 1f;
        go.GetComponent<Projectile>().direction = direction.normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        go.transform.rotation = Quaternion.AngleAxis(angle-90f, Vector3.forward);
    }
}

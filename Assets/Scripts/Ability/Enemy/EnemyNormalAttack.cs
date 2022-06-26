using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormalAttack : Ability
{
    public GameObject projectilePrefab;
    public override void Perform(Character caster, Character target)
    {
        caster.anim.SetTrigger("Attack");

        Vector3 direction = target.transform.position - new Vector3(caster.transform.position.x, caster.transform.position.y, 0f);

        GameObject go = GameObject.Instantiate(projectilePrefab, caster.transform.position, Quaternion.identity);
        go.GetComponent<Projectile>().caster = caster;
        go.GetComponent<Projectile>().lifetime = 10f;
        go.GetComponent<Projectile>().speed = 3f;
        go.GetComponent<Projectile>().damage = 1f;
        go.GetComponent<Projectile>().direction = direction.normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        go.transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiShoot : Ability
{
    public GameObject projectilePrefab;

    public override void Perform(Character caster)
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = new Vector3(worldPosition.x, worldPosition.y, 0f) - new Vector3(caster.transform.position.x, caster.transform.position.y, 0f);

        float rotateValue = -30f;
        for(int i = 0; i < 3; i++)
        {
            GameObject go = GameObject.Instantiate(projectilePrefab, caster.transform.position, Quaternion.identity);
            go.GetComponent<Projectile>().caster = caster;
            go.GetComponent<Projectile>().lifetime = 10f;
            go.GetComponent<Projectile>().speed = 5f;
            go.GetComponent<Projectile>().damage = 1f;
            Vector3 newDirection = Quaternion.Euler(0, 0, rotateValue) * direction;
            go.GetComponent<Projectile>().direction = newDirection.normalized;

            float angle = Mathf.Atan2(newDirection.y, newDirection.x) * Mathf.Rad2Deg;
            go.transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);

            rotateValue += 30f;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : Projectile
{
    public float radius;
    public override void Update()
    {
        timer += Time.deltaTime;

        if (timer > lifetime)
        {
            Destroy(gameObject);
            return;
        }

        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach(Collider2D col in cols)
        {
            if(col.CompareTag("Enemy"))
            {
                direction = (col.transform.position - transform.position).normalized;
            }
        }

        transform.position += direction * speed * Time.deltaTime;
    }
}

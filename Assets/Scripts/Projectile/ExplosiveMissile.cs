using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveMissile : Projectile
{
    public float radius;
    public GameObject explosiveVFX;
    public float vfxDuration;
    public override void Update()
    {
        timer += Time.deltaTime;

        if (timer > lifetime)
        {
            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, radius);
            foreach(Collider2D col in cols)
            {
                if (col.CompareTag("Enemy"))
                {
                    col.GetComponent<Enemy>().TakeDamage(damage);
                }
            }
            GameObject vfx = Instantiate(explosiveVFX, transform.position, Quaternion.identity);
            Destroy(vfx, vfxDuration);
            Destroy(gameObject);
            return;
        }

        transform.position += direction * speed * Time.deltaTime;
        transform.Rotate(0f, 0f, 360f * Time.deltaTime);
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, radius);
            foreach (Collider2D col in cols)
            {
                if (col.CompareTag("Enemy"))
                {
                    col.GetComponent<Enemy>().TakeDamage(damage);
                }
            }

            GameObject vfx = Instantiate(explosiveVFX, transform.position, Quaternion.identity);
            Destroy(vfx, vfxDuration);
            Destroy(gameObject);
        }
    }

}

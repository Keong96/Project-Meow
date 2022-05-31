using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    float timer;

    public float damage;
    public float lifetime;
    public float speed;
    public Vector3 direction;

    private void Start()
    {
        timer = 0f;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer > lifetime)
        {
            Destroy(gameObject);
            return;
        }

        transform.position += direction * speed * Time.deltaTime;
        transform.Rotate(0f, 0f, 360f * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

    }
}

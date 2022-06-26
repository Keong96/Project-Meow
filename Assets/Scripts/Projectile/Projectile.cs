using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetType
{
    Enemy,
    Player
}

public class Projectile : MonoBehaviour
{
    public float timer;
    public Character caster;
    public float damage;
    public float lifetime;
    public float speed;
    public Vector3 direction;
    public bool doSpin;
    public int numOfPierce;
    public TargetType targetType;

    int pierceCount;
/*  
    public Action OnSpawn;
    public Action OnUpdate;
    public Action OnDestroy;
*/

    private void Start()
    {
        timer = 0f;
        pierceCount = 0;
    }

    public virtual void Update()
    {
        timer += Time.deltaTime;

        if(timer > lifetime)
        {
            Destroy(gameObject);
            return;
        }

        transform.position += direction * speed * Time.deltaTime;

        if (doSpin)
            transform.Rotate(0f, 0f, 360f * Time.deltaTime);
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetType.ToString()))
        {
            collision.GetComponent<Character>().TakeDamage(damage);

            pierceCount++;
            if (pierceCount >= numOfPierce)
                Destroy(gameObject);
        }
        else if (collision.CompareTag("Projectile"))
        {
            pierceCount++;
            if (pierceCount >= numOfPierce)
                Destroy(gameObject);
        }
    }

    public virtual void OnTriggerExit2D(Collider2D collision)
    {

    }
}

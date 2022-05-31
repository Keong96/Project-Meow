using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public SpriteRenderer model;
    public GameObject projectilePrefab;

    [Header("Stats")]
    public Stat currentHealth;
    public Stat maxHealth;
    public Stat currentMana;
    public Stat maxMana;
    public Stat movementSpeed;
    public Stat jumpVelocity;

    public bool isImmune;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Init()
    {
        currentHealth.Init();
        maxHealth.Init();
        currentMana.Init();
        maxMana.Init();
        movementSpeed.Init();
        jumpVelocity.Init();
    }

    public void TakeDamage(float damage)
    {
        if (isImmune) return;

        currentHealth.currentValue -= damage;
        StartCoroutine(Flash());
    }

    public IEnumerator Flash() //Move to VisualHelper in future
    {
        isImmune = true;
        for (int i = 0; i < 5; i++)
        {
            if(model.color.a == 1f)
            {
                model.color = new Color(model.color.r, model.color.g, model.color.b, 0f);
            }
            else
            {
                model.color = new Color(model.color.r, model.color.g, model.color.b, 1f);
            }

            yield return new WaitForSeconds(0.1f);
        }

        model.color = new Color(model.color.r, model.color.g, model.color.b, 1f);
        isImmune = false;
    }
}

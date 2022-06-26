using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType
{
    Melee,
    Range
}

public class Enemy : Character
{
    [Header("Ability")]
    [SerializeReference] public Ability attackAbility;

    float attackTimer;

    public Character target;
    public AttackType attackType;
    public float attackRange;

    public override void Start()
    {
        base.Start();
        attackTimer = 0f;
    }

    public void Update()
    {
        attackTimer += Time.deltaTime;

        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, attackRange);
        target = null;

        foreach (Collider2D col in cols)
        {
            if(col.CompareTag("Player"))
            {
                target = col.GetComponent<Character>();
            }
        }

        if (target == null)
        {
            ChasePlayer();
        }
        else
        {
            Attack();
        }
    }

    public void ChasePlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player)
        {
            Vector3 direction = player.transform.position - transform.position;

            transform.position += direction.normalized * movementSpeed.currentValue * Time.deltaTime;

            if (direction.x > 0.15f)
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            else if (direction.x < -0.15f)
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
    }

    public void Attack()
    {
        Vector3 direction = target.transform.position - transform.position;

        if (direction.x > 0.15f)
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        else if (direction.x < -0.15f)
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

        if (attackTimer > attackAbility.cooldown)
        {
            attackAbility.Perform(this, target);
            attackTimer = 0f;
        }
    }
}

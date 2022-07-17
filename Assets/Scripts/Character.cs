using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public enum StatEnum
{
    currentHealth,
    maxHealth,
    currentMana,
    maxMana,
    movementSpeed,
    jumpVelocity,
    dashValue,
    attackDamage
}

public class Character : MonoBehaviour
{
    public SpriteRenderer model;
    public Animator anim;
    public GameObject weaponHolder;
    public GameObject armorHolder;

    [FoldoutGroup("Stats")] public Stat currentHealth;
    [FoldoutGroup("Stats")] public Stat maxHealth;
    [FoldoutGroup("Stats")] public Stat currentMana;
    [FoldoutGroup("Stats")] public Stat maxMana;
    [FoldoutGroup("Stats")] public Stat movementSpeed;
    [FoldoutGroup("Stats")] public Stat jumpVelocity;
    [FoldoutGroup("Stats")] public Stat dashValue;
    [FoldoutGroup("Stats")] public Stat attackDamage;
    [FoldoutGroup("Stats")] public Stat cooldownReduction;


    [FoldoutGroup("PlayerStatus")] public bool isImmune;
    [FoldoutGroup("PlayerStatus")] public bool onGround;
    [FoldoutGroup("PlayerStatus")] public bool canMove;
    [FoldoutGroup("PlayerStatus")] public bool canJump;
    [FoldoutGroup("PlayerStatus")] public bool canDash;
    [FoldoutGroup("PlayerStatus")] public bool canAttack;
    [FoldoutGroup("PlayerStatus")] [ReadOnly] public int numOfJump;
    [FoldoutGroup("PlayerStatus")] [ReadOnly] public int numOfDash;

    // Start is called before the first frame update
    public virtual void Start()
    {
        Init();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        onGround = Physics2D.Raycast(transform.position, Vector2.down, 1f, LayerMask.GetMask("Ground"));

        if(onGround)
        {
            if(numOfJump > 0)
                numOfJump = 0;

            if (numOfDash > 0)
                numOfDash = 0;
        }
    }

    void Init()
    {
        currentHealth.Init();
        maxHealth.Init();
        currentMana.Init();
        maxMana.Init();
        movementSpeed.Init();
        jumpVelocity.Init();
        dashValue.Init();
        numOfJump = 0;
        numOfDash = 0;

        canMove = true;
        canJump = true;
        canDash = true;
        canAttack = true;
    }

    public void ApplyMod(StatEnum statEnum, Modifier mod)
    {
        switch(statEnum)
        {
            case StatEnum.maxHealth:
                maxHealth.AddModifier(mod);
                break;
            case StatEnum.currentHealth:
                currentHealth.AddModifier(mod);
                break;
            case StatEnum.maxMana:
                maxMana.AddModifier(mod);
                break;
            case StatEnum.currentMana:
                currentMana.AddModifier(mod);
                break;
            case StatEnum.movementSpeed:
                movementSpeed.AddModifier(mod);
                break;
            case StatEnum.jumpVelocity:
                jumpVelocity.AddModifier(mod);
                break;
            case StatEnum.dashValue:
                dashValue.AddModifier(mod);
                break;
            case StatEnum.attackDamage:
                attackDamage.AddModifier(mod);
                break;
        }
    }

    public void RemoveMod(StatEnum statEnum, Modifier mod)
    {
        switch (statEnum)
        {
            case StatEnum.maxHealth:
                maxHealth.RemoveModifier(mod);
                break;
            case StatEnum.currentHealth:
                currentHealth.RemoveModifier(mod);
                break;
            case StatEnum.maxMana:
                maxMana.RemoveModifier(mod);
                break;
            case StatEnum.currentMana:
                currentMana.RemoveModifier(mod);
                break;
            case StatEnum.movementSpeed:
                movementSpeed.RemoveModifier(mod);
                break;
            case StatEnum.jumpVelocity:
                jumpVelocity.RemoveModifier(mod);
                break;
            case StatEnum.dashValue:
                dashValue.RemoveModifier(mod);
                break;
            case StatEnum.attackDamage:
                attackDamage.RemoveModifier(mod);
                break;
        }
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
            float h, s, v;
            Color.RGBToHSV(model.color, out h, out s, out v);
            if (s == 1f)
            {
                //model.color = new Color(model.color.r, model.color.g, model.color.b, 1f);
                model.color = Color.HSVToRGB(0.0f, 0.0f, 1.0f);
            }
            else
            {
                //model.color = new Color(model.color.r, model.color.g, model.color.b, 1f);
                model.color = Color.HSVToRGB(0.0f, 1.0f, 1.0f);
            }

            yield return new WaitForSeconds(0.1f);
        }

        model.color = Color.HSVToRGB(0.0f, 0.0f, 1.0f);
        isImmune = false;
    }
}

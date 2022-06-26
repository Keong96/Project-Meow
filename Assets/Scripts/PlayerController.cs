using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 movement;
    Rigidbody2D rb;
    Player player;
    float jumpTimer;
    float dashTimer;
    float leftClickTimer;
    float rightClickTimer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();

        jumpTimer = 0f;
        dashTimer = 0f;
        leftClickTimer = 0f;
        rightClickTimer = 0f;
    }

    private void Update()
    {
        jumpTimer += Time.deltaTime;
        dashTimer += Time.deltaTime;
        leftClickTimer += Time.deltaTime;
        rightClickTimer += Time.deltaTime;

        movement.Set(Input.GetAxisRaw("Horizontal"), 0f, 0f);

        if(player.canMove)
            Move();

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (!player.canJump) return;

            if(player.jumpAbility != null && jumpTimer > player.jumpAbility.cooldown)
            {
                player.jumpAbility.Perform(player);
                jumpTimer = 0f;
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (!player.canDash) return;

            if (player.dashAbility != null && dashTimer > player.dashAbility.cooldown)
            {
                player.dashAbility.Perform(player);
                dashTimer = 0f;
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if(player.leftClickAbility != null && leftClickTimer > player.leftClickAbility.cooldown)
            {
                player.leftClickAbility.Perform(player);
                leftClickTimer = 0f;
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            if(player.rightClickAbility != null && rightClickTimer > player.rightClickAbility.cooldown)
            {
                player.rightClickAbility.Perform(player);
                rightClickTimer = 0f;
            }
        }
    }

    public void Move()
    {
        if (Mathf.Abs(movement.x) > 0.15f)
        {
            transform.position += movement * player.movementSpeed.currentValue * Time.deltaTime;

            if(movement.x > 0.15f)
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            else if (movement.x < -0.15f)
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

            player.anim.SetBool("IsMoving", true);
        }
        else
        {
            player.anim.SetBool("IsMoving", false);
        }  
    }
}

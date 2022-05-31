using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 movement;
    Rigidbody2D rb;
    Player player;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
    }

    private void Update()
    {
        movement.Set(Input.GetAxisRaw("Horizontal"), 0f, 0f);

        Move();

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if(player.jumpAbility != null)
                player.jumpAbility.Perform(player);
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if(player.leftClickAbility != null)
                player.leftClickAbility.Perform(player);
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            if(player.rightClickAbility != null)
                player.rightClickAbility.Perform(player);
        }
    }

    public void Move()
    {
        transform.position += movement * player.movementSpeed.currentValue * Time.deltaTime;
    }
}

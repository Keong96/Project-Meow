using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollide : MonoBehaviour
{
    public Player player;
    public GameObject vfxPrefab;

    public void Start()
    {
        player = GetComponentInParent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (!collision.GetComponent<Character>().isImmune)
            {
                GameObject vfx = Instantiate(vfxPrefab, collision.transform.position, Quaternion.identity);
                Destroy(vfx, 0.15f);
                collision.GetComponent<Character>().TakeDamage(player.attackDamage.currentValue);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Player player;
    public EquipmentSO weapon;
    public GameObject vfxPrefab;

    private void Start()
    {
        player = GetComponentInParent<Player>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = new Vector3(worldPosition.x, worldPosition.y, 0f) - new Vector3(transform.position.x, transform.position.y, 0f);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            if(!collision.GetComponent<Character>().isImmune)
            {
                GameObject vfx = Instantiate(vfxPrefab, collision.transform.position, Quaternion.identity);
                Destroy(vfx, 0.15f);
                collision.GetComponent<Character>().TakeDamage(player.attackDamage.currentValue);
            }
        }
    }
}

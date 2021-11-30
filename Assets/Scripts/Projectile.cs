using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float aoeRadius;
    public LayerMask enemyLayers;
    private int damage;

    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(
            collision.gameObject.transform.position,
            aoeRadius,
            enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}

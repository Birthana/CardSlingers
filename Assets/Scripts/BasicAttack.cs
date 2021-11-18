using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    [Header("Player Properties")]
    public MageHand player;
    public int baseDamage;

    [Header("Attack Properties")]
    public Transform attackPoint;
    public float length, width;
    public LayerMask enemyLayers;
    private bool isAttacking = false;

    private void Update()
    {
        CheckBasicAttack();
    }

    private void CheckBasicAttack()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isAttacking)
        {
            StartCoroutine(Attacking());
        }
    }

    IEnumerator Attacking()
    {
        isAttacking = true;
        yield return new WaitForSeconds(0.5f);
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackPoint.position, new Vector2(width, length), 0f, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            Trigger trigger = player.TriggerCheck();
            int damage = trigger.PerformTrigger(baseDamage);
            enemy.GetComponent<Health>().TakeDamage(damage);
        }
        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(attackPoint.position, new Vector2(width, length));
    }
}

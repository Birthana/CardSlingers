using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    [Header("Player Properties")]
    public MageHand player;
    public int baseDamage;
    public float knockBackForce = 5f;

    [Header("Attack Properties")]
    public Transform attackPoint;
    public float length, width;
    public LayerMask enemyLayers;
    public float attackDelay = 0.5f;
    private bool isAttacking = false;

    private void Update()
    {
        CheckBasicAttack();
    }

    private void CheckBasicAttack()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isAttacking)
        {
            StartCoroutine(Attacking());
        }
    }

    IEnumerator Attacking()
    {
        isAttacking = true;
        Vector3 direction = GetDirectionFromMouse();
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(
            direction + transform.position, 
            new Vector2(width, length), 
            Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg, 
            enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            Trigger trigger = (player.NoCardsLeft()) ? null : player.TriggerCheck();
            int damage = (trigger == null) ? baseDamage : trigger.PerformTrigger(baseDamage);
            enemy.GetComponent<Health>().TakeDamage(damage);
            enemy.GetComponent<Enemy>().KnockBack(knockBackForce);
        }
        yield return new WaitForSeconds(attackDelay);
        isAttacking = false;
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            return;
        Vector3 direction = GetDirectionFromMouse();
        Gizmos.matrix = Matrix4x4.TRS(
            direction + transform.position,
            Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg),
            Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, new Vector2(width, length));
    }

    private Vector3 GetDirectionFromMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        Vector3 direction = (mousePosition - transform.position).normalized;
        return direction;
    }
}

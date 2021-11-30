using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float stoppingDistance = 0.5f;
    public float moveSpeed;
    public float knockBackDecay = 1f;

    private bool isStunned = false;
    private Vector3 outsideForces;
    private Rigidbody2D rb;
    private Transform target;

    private Coroutine stunStatus;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<PlayerMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (isStunned)
            return;
        Vector3 direction = (target.position - transform.position).normalized;
        if (Vector3.Distance(transform.position, target.position) > stoppingDistance)
        {
            rb.velocity = (direction * moveSpeed) + outsideForces;
        }
        else
        {
            rb.velocity = Vector3.zero + outsideForces;
        }

        outsideForces = Vector3.Lerp(outsideForces, Vector3.zero, knockBackDecay * Time.deltaTime);
    }

    public void KnockBack(float knockBackForce)
    {
        outsideForces = (transform.position - target.position).normalized * knockBackForce;
    }

    public void Stun(float stunDuration)
    {
        rb.velocity = Vector3.zero;
        isStunned = true;
        if (stunStatus != null)
            StopCoroutine(stunStatus);
        stunStatus = StartCoroutine(Stunned(stunDuration));
    }

    IEnumerator Stunned(float stunDuration)
    {
        yield return new WaitForSeconds(stunDuration);
        isStunned = false;
    }
}

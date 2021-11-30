using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fireball", menuName = "Card/Fireball")]
public class Fireball : Card
{
    public Projectile fireballPrefab; 
    public float projectileSpeed;
    public float projectileLifeTime;

    public override void Cast()
    {
        Vector3 playerPosition = FindObjectOfType<PlayerMovement>().transform.position;
        Projectile newProjectile = Instantiate(fireballPrefab);
        newProjectile.gameObject.transform.position = playerPosition;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        Vector3 direction = (mousePosition - newProjectile.transform.position).normalized;
        newProjectile.gameObject.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed * 3;
        newProjectile.SetDamage(damage);
        Destroy(newProjectile.gameObject, projectileLifeTime);
    }
}

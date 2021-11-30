using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IntenseHeat", menuName = "Card/IntenseHeat")]
public class IntenseHeat : Card
{
    public float stunDuration = 1.0f;

    public override bool CanCast()
    {
        var mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D mouseHit = Physics2D.Raycast(mouseRay.origin, Vector2.zero);
        return mouseHit;
    }

    public override void Cast()
    {
        var mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D mouseHit = Physics2D.Raycast(mouseRay.origin, Vector2.zero);
        if (mouseHit)
        {
            Enemy enemy = mouseHit.collider.gameObject.GetComponent<Enemy>();
            if (enemy)
            {
                enemy.Stun(stunDuration);
            }
        }
    }
}

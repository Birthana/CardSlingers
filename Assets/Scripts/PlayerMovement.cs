using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 1f;
    private Animator anim;
    private string currentState;
    private Rigidbody2D rb;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentState = "Player_Walk_Down";
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(h, v, 0).normalized;

        rb.velocity = dir * moveSpeed;

        //transform.Translate(dir * moveSpeed * Time.deltaTime, Space.World);
        /**
        if (h > 0)
        {
            ChangeAnimation("Player_Walk_Right");
        }
        else if (h < 0)
        {
            ChangeAnimation("Player_Walk_Left");
        }
        else if (v > 0)
        {
            ChangeAnimation("Player_Walk_Up");
        }
        else if (v < 0)
        {
            ChangeAnimation("Player_Walk_Down");
        }
        **/
    }

    private void ChangeAnimation(string state)
    {
        if (state.Equals(currentState))
            return;
        anim.Play(state);
        currentState = state;
    }
}

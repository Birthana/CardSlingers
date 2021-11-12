using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 1f;

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        
        if (h == 1)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime, Space.World);
        }
        else if (h == -1)
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime, Space.World);
        }
        
        if (v == 1)
        {
            transform.Translate(Vector2.up * moveSpeed * Time.deltaTime, Space.World);
        }
        else if (v == -1)
        {
            transform.Translate(Vector2.down * moveSpeed * Time.deltaTime, Space.World);
        }
    }
}

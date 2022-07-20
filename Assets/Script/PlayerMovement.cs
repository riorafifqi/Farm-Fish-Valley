using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    public float speed = 5f;

    private Vector2 movement;
    private KeyCode key;
    private bool hold;
    private SpriteRenderer playerSpriteRenderer;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        key = KeyCode.None;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W)) key = KeyCode.W;
        else if (Input.GetKey(KeyCode.A)) key = KeyCode.A;
        else if (Input.GetKey(KeyCode.S)) key = KeyCode.S;
        else if (Input.GetKey(KeyCode.D)) key = KeyCode.D;
        
        if (Input.GetKeyUp(key)) key = KeyCode.None;

        switch (key)
        {
            case KeyCode.W: 
                movement.y = 1;
                movement.x = 0;
                break;
            case KeyCode.A: 
                movement.x = -1;
                movement.y = 0;
                break;
            case KeyCode.S: 
                movement.y = -1;
                movement.x = 0;
                break;
            case KeyCode.D: 
                movement.x = 1;
                movement.y = 0;
                break;
            default: movement = Vector2.zero; break;
        }

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        if (animator.GetFloat("Horizontal") < 0)
        {
            playerSpriteRenderer.flipX = true;
        }
        else if(animator.GetFloat("Horizontal") > 0)
        {
            playerSpriteRenderer.flipX = false;
        }
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
    }
}

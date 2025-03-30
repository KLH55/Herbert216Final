using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 movementVector;
    private Animator animator;
    private SpriteRenderer sprite_r;
    private Rigidbody2D rb;
    private bool isGrounded = false;
    private bool jump = false;
    private bool facingRight = true;

    public float speed = 3;
    public float height = 500;
    public float maxSpeed = 7f;
    public float gravMult = 3f;
    // Start is called before the first frame update
    void Start()
    {
        sprite_r = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
     

        if (movementVector.x > 0 && rb.velocity.x < maxSpeed)
            rb.AddForce(Vector2.right * speed);
        else if (movementVector.x < 0 && Mathf.Abs(rb.velocity.x) < maxSpeed)
            rb.AddForce(Vector2.left * speed);

        if (movementVector.x < 0 && facingRight)
        {
            Flip();
            facingRight = false;
        }
        else if (movementVector.x > 0 && !facingRight)
        {
            Flip();
            facingRight = true;
        }

        if (jump)
        {
            rb.AddForce(Vector2.up * height);
            jump = false;
            isGrounded = false;
        }

        if (rb.velocity.y < 0)
            rb.gravityScale = gravMult;
        else
            rb.gravityScale = 1;

    }

    public void OnMove(InputValue movementValue)
    {
        movementVector = movementValue.Get<Vector2>();
        Debug.Log(movementVector.x);
    }

    public void OnJump(InputValue movementValue)
    {
        if (isGrounded)
            jump = true;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            Debug.Log("Touching GHround");
        }
    }

    public Vector2 GetDirection()
    {
        if (facingRight)
            return Vector2.right;
        else
            return Vector2.left;
    }

    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x = theScale.x * -1;
        transform.localScale = theScale;
    }
}

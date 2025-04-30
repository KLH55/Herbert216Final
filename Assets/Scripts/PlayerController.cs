// Kris Herbert 4/29/2025; This code is what makes the player character work. It allows movement, attacking, jumping,
// and all the things that can kill or heal the player.

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Vector2 movementVector;
    private Animator animator;
    private SpriteRenderer sprite_r;
    private Rigidbody2D rb;
    private bool isGrounded = false;
    private bool jump = false;
    private bool facingRight = true;
    private AudioSource audioSource;

    public float speed = 3;
    public float height = 500;
    public float maxSpeed = 7f;
    public float gravMult = 3f;
    public Transform atkPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        sprite_r = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update used to check if the player pressed the Q key to attack.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            animator.SetBool("IsAttacking", true);
            RaycastHit2D hit = Physics2D.Raycast(atkPoint.position, Vector2.right, 1.35f);
            Debug.DrawRay(atkPoint.position, new Vector2(1.35f, 0), Color.red, 0.5f);
            Debug.Log(hit);
            if (hit.collider != null && hit.collider.gameObject.CompareTag("Enemy"))
            {
                Destroy(hit.collider.gameObject);
                animator.SetBool("IsAttacking", false);
            }
        }
    }

    // FixedUpdate used for movement and jumping.
    void FixedUpdate()
    {
        animator.SetFloat("Speed", Mathf.Abs(movementVector.x));

        if (movementVector.x > 0 && rb.velocity.x < maxSpeed) // Moves player left and right
            rb.AddForce(Vector2.right * speed);
        else if (movementVector.x < 0 && Mathf.Abs(rb.velocity.x) < maxSpeed)
            rb.AddForce(Vector2.left * speed);

        if (movementVector.x < 0 && facingRight) // Flips the player sprite to face left or right
        {
            Flip();
            facingRight = false;
        }
        else if (movementVector.x > 0 && !facingRight)
        {
            Flip();
            facingRight = true;
        }

        if (jump) // Checks to see if the player has jumped
        {
            rb.AddForce(Vector2.up * height);
            jump = false;
            isGrounded = false;
        }

        if (rb.velocity.y < 0) // Makes player less floaty
            rb.gravityScale = gravMult;
        else
            rb.gravityScale = 1;

    }

    // Function used to check if the A or D key is pressed.
    public void OnMove(InputValue movementValue) // Uses the keyboard input A or D to have player move left or right
    {
        movementVector = movementValue.Get<Vector2>();
        Debug.Log(movementVector.x);
    }

    // Function to check if spacebar key is pressed.
    public void OnJump(InputValue movementValue) // Uses the keyboard input spacebar to have the player jump
    {
        if (isGrounded)
        {
            animator.SetBool("IsJumping", true);
            jump = true;
        }

    }

    // Checks for collision.
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // Checks to see if the player is in contact with the ground.
        {
            animator.SetBool("IsJumping", false);
            isGrounded = true;
            Debug.Log("Touching Ground");
        }
        if (collision.gameObject.CompareTag("Orb")) // Checks to see if the player has been hit by the Orb attack.
        {
            GameManager.instance.DecreaseLives();
            SceneManager.LoadScene("Long Path");
        }
    }

    // Used to check if the player has fallen out of bounds on the map.
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boundry"))
        {
            GameManager.instance.DecreaseLives();
            SceneManager.LoadScene("Long Path");
        }
    }

    // Checks if the player is facing left or right
    public Vector2 GetDirection()
    {
        if (facingRight)
            return Vector2.right;
        else
            return Vector2.left;
    }

    // Flips the player sprite to face left or right
    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x = theScale.x * -1;
        transform.localScale = theScale;
    }
}

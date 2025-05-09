// Kris Herbert 4/29/2025; This code is used for the Wizard Apprentice enemy and makes this enemy move and shoot a magic orb when the player is nearby.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApprenticeController : MonoBehaviour
{
    public GameObject orb;
    public Transform firePoint;

    private SpriteRenderer rend;
    private Animator animator;
    private int direction = 1;
    private float fireRate = 1f;
    private float nextFire = 1.5f;
    private bool facingRight = true;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Fixed update is used to check if the apprentice can see the player to shoot at them,
    // it is also used fot the apprentice to walk on platfroms.
    void FixedUpdate()
    {
        animator.SetFloat("Speed", Mathf.Abs(direction));
        RaycastHit2D appHit = Physics2D.Raycast(transform.position, Vector2.down, 1f);
        Debug.DrawRay(transform.position, new Vector2(0, -2), Color.red, 0.5f);
        Debug.Log(appHit);

        RaycastHit2D playerHit = Physics2D.Raycast(transform.position, Vector2.left, 4f);
        Debug.DrawRay(transform.position, new Vector2(-4, 0), Color.red, .5f);
        Debug.Log(playerHit);

        if (appHit.collider == null)
        {
            direction = direction * -1;
            rend.flipX = !rend.flipX;
        }

        if (playerHit.collider != null && playerHit.collider.CompareTag("Player"))
        {
            OnOrb();
            audioSource.Play();
        }

        transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x - 1 * direction, transform.position.y), Time.deltaTime);
    }

    // Function to create the orb that is shot at the player.
    public void OnOrb()
    {
        if (Time.time >= nextFire)
        {
            nextFire = Time.time + fireRate;
            animator.SetBool("IsShooting", true);
            Instantiate(orb, firePoint.position, facingRight ? firePoint.rotation : Quaternion.Euler(0, 180, 0));
        }
    }

    // Checks to see if the apprentice is facing left or right.
    public Vector2 GetDirection()
    {
        if (facingRight)
            return Vector2.right;
        else
            return Vector2.left;
    }
}

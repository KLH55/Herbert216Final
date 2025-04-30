// Kris Herbert 4/29/2025; This code is for the Bandit enemy and makes this enemy walk and do a melee attack against the player.

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BanditController : MonoBehaviour
{
    private int direction = 1;
    private SpriteRenderer rend;
    private Animator animator;

    public Transform atkPoint;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is used to check if the player is within range of the Bandit to be attacked.
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(atkPoint.position, Vector2.left, 1.35f);
        Debug.DrawRay(atkPoint.position, new Vector2(-1.35f, 0), Color.red, .5f);
        if (hit.collider != null && hit.collider.gameObject.CompareTag("Player"))
        {
            animator.SetBool("IsAttacking", true);
            GameManager.instance.DecreaseLives();
            SceneManager.LoadScene(0);
        }
    }

    // FixedUpdate is used for the Bandit's movement on platforms.
    void FixedUpdate()
    {
        animator.SetFloat("Speed", Mathf.Abs(direction));
        RaycastHit2D banHit = Physics2D.Raycast(transform.position, Vector2.down, 1f);
        Debug.DrawRay(transform.position, new Vector2(0, -2), Color.red, 0.5f);
        Debug.Log(banHit);

        if (banHit.collider == null)
        {
            direction = direction * -1;
            rend.flipX = !rend.flipX;
        }

        transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x - 1 * direction, transform.position.y), Time.deltaTime);
    }
}

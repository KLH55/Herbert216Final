using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BanditController : MonoBehaviour
{
    private int direction = 1;
    private SpriteRenderer rend;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    
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

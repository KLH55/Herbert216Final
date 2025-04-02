using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApprenticeController : MonoBehaviour
{
    private int direction = 1;
    private int health = 80;
    private SpriteRenderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    
    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f);
        Debug.DrawRay(transform.position, new Vector2(0, -2), Color.red, 0.5f);
        Debug.Log(hit);

        if (hit.collider == null)
        {
            direction = direction * -1;
            rend.flipX = !rend.flipX;
        }

        transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x - 1 * direction, transform.position.y), Time.deltaTime);
    }
}

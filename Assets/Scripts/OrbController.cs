using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbController : MonoBehaviour
{
    public float force = 16f;

    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        ApprenticeController wizard = GameObject.FindAnyObjectByType<ApprenticeController>();
        direction = wizard.GetDirection();
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = force * direction * -1;
        Invoke("Die", 2f);
    }

    void Die()
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbController : MonoBehaviour
{
    public float force = 6f;

    private Vector2 direction;

    // Start is called before the first frame update. Used to make, move, and destroy an orb object that the apprentice fires at the player.
    void Start()
    {
        ApprenticeController wizard = GameObject.FindAnyObjectByType<ApprenticeController>();
        direction = wizard.GetDirection();
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = force * direction * -1;
        Invoke("Die", 2f);
    }

    // Destroys the orb just shot.
    void Die()
    {
        Destroy(gameObject);
    }
}

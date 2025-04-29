using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotionController : MonoBehaviour
{
    // Increases the player's lives when they touch a health potion.
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            GameManager.instance.IncreaseLives();
            Debug.Log(GameManager.instance.GetLives());
        }
    }
}

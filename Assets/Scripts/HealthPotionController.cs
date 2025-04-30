// Kris Herbert 4/29/2025; This code is for the health potion pickup that adds lives for the player.

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

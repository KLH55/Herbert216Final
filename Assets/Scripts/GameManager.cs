// Kris Herbert 4/29/2025; This game manager is used to keep track of the players lives.

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int lives = 3;

    // Awake used to make sure the game is loaded properly.
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    // Function used to check if the players lives are less than or equal to zero then quits the game if they are.
    public void NoLives()
    {
        if (lives <= 0)
            EditorApplication.isPlaying = false;
    }

    // Subtracts one from the number of lives the player has.
    public void DecreaseLives()
    {
        lives--;
        NoLives();
    }

    // Adds one to the number of lives the player has.
    public void IncreaseLives()
    {
        lives++;
    }

    // Used to see how many lives the player has.
    public int GetLives()
    {
        return lives;
    }
}

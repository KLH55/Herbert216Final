// Kris Herbert 4/29/2025; Used to load into the game from Start Screen.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public string newGame;
    
    // Loads the game from the Start Screen scene.
    public void StartingGame()
    {
        SceneManager.LoadScene(newGame);
    }
}

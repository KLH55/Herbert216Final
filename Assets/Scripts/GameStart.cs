using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public string newGame;
    
    public void StartingGame()
    {
        SceneManager.LoadScene(newGame);
    }
}

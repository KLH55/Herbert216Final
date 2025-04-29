using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    public void QuittingGame()
    {
        EditorApplication.isPlaying = false;
    }
}

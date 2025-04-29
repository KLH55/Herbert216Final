using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LivesController : MonoBehaviour
{
    // Used to update the lives UI.
    public TMP_Text livesTxt;
    // Start is called before the first frame update
    void Start()
    {
        livesTxt.SetText("Lives: " + GameManager.instance.GetLives());
    }
}

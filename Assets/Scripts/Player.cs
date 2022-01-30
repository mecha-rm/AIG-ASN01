using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the player script.
public class Player : MonoBehaviour
{
    // the player's mouse.
    public Mouse2D mouse;

    // Start is called before the first frame update
    void Start()
    {
        // finds mouse if not set.
        if (mouse == null)
            mouse = FindObjectOfType<Mouse2D>();

        // mouse does not exist, so make one.
        if (mouse == null)
            mouse = gameObject.AddComponent<Mouse2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

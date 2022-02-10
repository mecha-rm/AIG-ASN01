using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// the door entry
public struct DoorEntry
{
    // if 'true', the door is hot.
    public bool hot;

    // if 'true', noise is heard behind the door.
    public bool noisy;

    // if 'true', the door is safe.
    public bool safe;

    // percentage of doors (0.0 - 1.0)
    public float percent;
}

// a door object for the game. 
public class Door : MonoBehaviour
{
    // if 'true', the door is hot.
    public bool hot = false;

    // if 'true', noise is heard behind the door.
    public bool noisy = false;

    // if 'true', the door is safe. If 'false', it's not safe.
    public bool safe = true;

    // if 'true', the door is open.
    public bool open = false;

    [Header("Sprites")]
    // sprite for a closed door
    public SpriteRenderer closedSprite;

    // sprite for an open door.
    public SpriteRenderer openSprite;

    [Header("Other")]

    // the gameplay manager for the object.
    GameplayManager manager;

    // the collider for the door.
    // Collider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        // TODO: search for door.
        if(closedSprite == null || openSprite == null)
        {
            SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();

            // sets closed sprites tofirst sprite.
            closedSprite = sprites[0];
            openSprite = sprites[1];
            
        }

        // finds the gameplay manager.
        if (manager == null)
            manager = FindObjectOfType<GameplayManager>();
    }

    // called when the door is collided with.
    private void OnTriggerEnter(Collider other)
    {
        // triggered by an explosion, so it can't kill the player.
        OpenDoor(false);
    }

    // sets the door with an entry.
    public void SetDoor(DoorEntry entry)
    {
        hot = entry.hot;
        noisy = entry.noisy;
        safe = entry.safe;
    }

    // opens the door. If 'directOpen' is 'true', then the door was opened directly by the player.
    public void OpenDoor(bool directOpen)
    {
        // door is already open, so do nothing.
        if (open)
            return;


        open = true;

        // toggle sprites.
        closedSprite.gameObject.SetActive(false);
        openSprite.gameObject.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

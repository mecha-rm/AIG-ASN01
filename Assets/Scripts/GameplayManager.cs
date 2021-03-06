using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// the gameplay manager.
public class GameplayManager : MonoBehaviour
{
    // the mouse from the gameplay manager.
    public Mouse2D mouse;

    // the file is set.
    public string file = "";

    // the file is not set.
    public string filePath = "";

    // user interface
    [Header("UI")]

    // text for doors oepned.
    public Text doorsOpenedText;

    // text to show that the door emits heat.
    public Text hotText;

    // text to show that there's noise being heard.
    public Text noisyText;

    // the win text for the game.
    public Text winText;

    // the lose text for the game.
    public Text loseText;

    // the door objects.
    [Header("Door")]

    // default door amount.
    private int defaultDoorCount = 36;

    // the list of doors.
    public List<Door> doors;

    // [HideInInspector]
    // the list of unsafe doors.
    public List<Door> unsafeDoors = new List<Door>();

    // the amount of doors that have been opened.
    public int doorsOpened = 0;

    // the amount of unsafe doors that have been opened.
    public int unsafeDoorsOpened = 0;

    // the prefab for the door.
    public GameObject doorPrefab;

    // the prefab for the explosion.
    public GameObject explosionPrefab;

    // spacing.
    public Vector2 spacing = new Vector2(1.0F, 1.0F);

    // the parent for the door object.
    public GameObject doorParent;

    // Start is called before the first frame update
    void Start()
    {
        // tries to find the mouse if not set.
        if (mouse == null)
        {
            mouse = FindObjectOfType<Mouse2D>();

            // adds a mouse.
            if (mouse == null)
                mouse = gameObject.AddComponent<Mouse2D>();
        }
        
        // finds the game loader in the scene.
        GameLoader loader = FindObjectOfType<GameLoader>();

        // the door entries.
        List<DoorEntry> doorEntries = new List<DoorEntry>();

        // the amount of doors.
        int doorCount = 0;

        // the door prefab is null, so try to load the prefab.
        if (doorPrefab == null)
        {
            // gets the prefab.
            object prefab = Resources.Load("Prefab/Door");

            // saves prefab.
            if (prefab != null)
            {
                doorPrefab = (GameObject)prefab;
            }
            else
            {
                Debug.LogError("No door base exists. Cannot run game.");
                return;
            }
        }

        // tries to find the parent if it's not set.
        if (doorParent == null)
        {
            // finds the door parent.
            doorParent = GameObject.Find("Doors");

            // makes a new object.
            if (doorParent == null)
                doorParent = new GameObject("Doors");

        }
            

        // loader exists, so grab information from there.
        if (loader != null)
        {
            doorCount = loader.doorCount;
            doorEntries = loader.doorsEntries;
        }
        
        // if there are no doors, or there are no door entries.
        if(doorCount <= 0 || doorEntries.Count == 0)
        {
            // 6 X 6
            if(doorCount <= 0)
                doorCount = defaultDoorCount;

            // random door entries (8 options possible).
            if(doorEntries.Count == 0)
            {
                for (int i = 0; i < 8; i++)
                {
                    DoorEntry entry = new DoorEntry();

                    entry.hot = (Random.Range(0, 2) == 1);
                    entry.noisy = (Random.Range(0, 2) == 1);
                    entry.safe = (Random.Range(0, 2) == 1);
                    entry.percent = 1.0F;

                    doorEntries.Add(entry);
                }
            }        

        }

        // TODO: randomize entry positions.

        // the sum of all the percents
        float percentSum = 0.0F;

        // adds together all of the percentages for probability distribution.
        foreach (DoorEntry entry in doorEntries)
            percentSum += entry.percent;

        // the row size.
        int rowLength = Mathf.CeilToInt(Mathf.Sqrt(doorCount));

        // the current column in the row.
        int rowIndex = 0;
        int colIndex = 0;

        // generates all of the doors.
        for(int i = 0; i < doorCount; i++)
        {
            // door object.
            GameObject doorObject = Instantiate(doorPrefab);
            Door door;

            // tries to get the door component, adding it if it's not set.
            if(!doorObject.TryGetComponent<Door>(out door))
                door = doorObject.AddComponent<Door>();

            // the current percent value.
            float percentValue = (percentSum / doorCount) * i;

            // the percent range.
            float percentThreshold = 0.0F;

            // applies door chance rate.
            for (int j = 0; j < doorEntries.Count; j++)
            {
                // ups the threshol.
                percentThreshold += doorEntries[j].percent;

                // if the threshold has not been past, set that as the value.
                if (percentValue < percentThreshold)
                {
                    door.SetDoor(doorEntries[j]);
                    break;
                }          
            }

            // TRANSFORM
            // sets the parent transform
            if (doorParent != null)
            {
                door.transform.parent = doorParent.transform;
                door.transform.localPosition = Vector3.zero;
            }

            // creates the position offset.
            Vector2 doorPosOffset = new Vector2(
                spacing.x * door.transform.localScale.x * colIndex,
                -spacing.y * door.transform.localScale.y * rowIndex
                );

            // moves the door to the right place.
            door.transform.Translate(doorPosOffset);

            // add door to list.
            doors.Add(door);

            // if the door is unsafe, add it to the list.
            if (!door.safe)
                unsafeDoors.Add(door);

            // increase the column count
            colIndex++;

            // at the end of the row, so move onto the next row.
            if(colIndex >= rowLength)
            {
                rowIndex++;
                colIndex = 0;
            }            
        }

        // scale down all doors (default count is 32)
        if(doorParent != null)
        {
            Vector3 scaleMult = Vector3.one;

            if (doorCount > 192) // 192 doors
            {
                scaleMult = new Vector3(0.6F, 0.6F, 1.0F);
            }
            else if(doorCount > 160) // 160 doors
            {
                scaleMult = new Vector3(0.6F, 0.6F, 1.0F);
            }
            if(doorCount > 128) // 128 doors
            {
                scaleMult = new Vector3(0.8F, 0.8F, 1.0F);
            }
            else if (doorCount > 96) // 96 doors
            {
                scaleMult = new Vector3(0.8F, 0.8F, 1.0F);
            }
            else if (doorCount > 64) // 64 doors
            {
                scaleMult = new Vector3(1.0F, 1.0F, 1.0F);
            }

            doorParent.transform.localScale.Scale(scaleMult);
        }

        // destroys the loader.
        if(loader != null)
            Destroy(loader.gameObject);
    }

    // a hot door was opened.
    // NOTE: I was not able to get this working, but it was supposed to open up adjacent doors.
    private void OnOpenedHotDoor(Door openedDoor)
    {
        // explosion prefab not set.
        if(explosionPrefab == null)
        {
            // loads the prefab.
            explosionPrefab = (GameObject)Resources.Load("Prefabs/Explosion");
        }

        // no prefab was found, so no explosion.
        if (explosionPrefab == null)
            return;

        // instantiates the object.
        GameObject newObject = Instantiate(explosionPrefab);

        // explosion component.
        Explosion explosion = null;

        // tries to get the component.
        if (!newObject.TryGetComponent<Explosion>(out explosion))
            explosion = newObject.AddComponent<Explosion>();

        // sets position.
        explosion.transform.position = openedDoor.transform.position;

        // QUICK FIX - manually open doors.
        // This doesn't match up with edge cases.
        {
            // finds the index of the opened door.
            int index = doors.IndexOf(openedDoor);

            // the doors hit.
            List<Door> hitDoors = new List<Door>();

            // left
            if(index - 1 > 0)
            {
                hitDoors.Add(doors[index - 1]);
            }

            // right
            if (index + 1 < doors.Count)
            {
                hitDoors.Add(doors[index + 1]);
            }

            // opens the doors.
            foreach(Door hitDoor in hitDoors)
            {
                // door isn't open, so open it.
                if (!hitDoor.open)
                {
                    // opens the door, and increments the opened door counter.
                    hitDoor.OpenDoor();
                    doorsOpened++;

                    // if the door was unsafe to open, you don't get a gameover, but it does open.
                    if (!hitDoor.safe)
                        unsafeDoorsOpened++;
                }
            }
            
        }
    }

    // a noisy door as opened.
    private void OnOpenedNoisyDoor(Door openedDoor)
    {
        // opens 1 to 5 random doors.
        int randNum = Random.Range(1, 6);
        randNum *= (Mathf.CeilToInt((float)doors.Count / defaultDoorCount));

        // opens up random doors.
        for(int i = 0; i < randNum; i++)
        {
            int index = Random.Range(0, doors.Count);

            // door isn't open, so open it.
            if(!doors[index].open)
            {
                // opens the door, and increments the opened door counter.
                doors[index].OpenDoor();
                doorsOpened++;

                // if the door was unsafe to open, you don't get a gameover, but it does open.
                if (!doors[index].safe)
                    unsafeDoorsOpened++;
            }
        }
    }

    // called for a game over.
    public void GameOver(bool win)
    {
        // disable mouse.
        mouse.enabled = false;

        // enables different message based onr results.
        if (win)
            winText.gameObject.SetActive(true);
        else
            loseText.gameObject.SetActive(true);
    }

    // returns to the title screen
    public void ReturnToTitle()
    {
        // returns to the title scene.
        SceneHelper.ChangeScene("TitleScene");
    }

    // Update is called once per frame
    void Update()
    {
        // player has clicked on something.
        if(mouse.clickedObject != null)
        {
            // the door object.
            Door door;
            
            // tries to get the door component from the object.
            if(mouse.clickedObject.TryGetComponent<Door>(out door))
            {
                // the door is closed, so open it.
                if(!door.open)
                {
                    // opens the door, and increments the opened door counter.
                    door.OpenDoor();
                    doorsOpened++;

                    // the door was not safe, so the player got a game over.
                    if (!door.safe)
                    {
                        unsafeDoorsOpened++;
                        GameOver(false);
                    }
                    else // only triggered if the door is safe.
                    {
                        // if the door is hot.
                        if (door.hot)
                        {
                            OnOpenedHotDoor(door);
                        }

                        // if the door is noisy.
                        if (door.noisy)
                        {
                            OnOpenedNoisyDoor(door);
                        }
                    }
                    
                }
            }

        }

        // hide text.
        hotText.gameObject.SetActive(false);
        noisyText.gameObject.SetActive(false);

        // if the mouse is hovering over something.
        if (mouse.hoveredObject != null)
        {
            // the door object.
            Door door;

            // tries to get the door component from the object.
            if (mouse.hoveredObject.TryGetComponent<Door>(out door))
            {
                // the door is closed, so open it.
                if (!door.open)
                {
                    hotText.gameObject.SetActive(door.hot);
                    noisyText.gameObject.SetActive(door.noisy);
                }
            }
        }

        // if there are no safe doors left.
        if ((doors.Count - unsafeDoors.Count) <= (doorsOpened - unsafeDoorsOpened))
            GameOver(true);


        // doors opened text.
        doorsOpenedText.text = doorsOpened.ToString() + "/" + doors.Count.ToString();
    }
}

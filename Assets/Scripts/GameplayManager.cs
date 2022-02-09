using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the gameplay manager.
public class GameplayManager : MonoBehaviour
{
    // the door objects.
    [Header("Door")]

    // the list of doors.
    public List<Door> doors;

    // the prefab for the door.
    public GameObject doorPrefab;

    // spacing.
    public Vector2 spacing = new Vector2(1.0F, 1.0F);

    // the parent for the door object.
    public GameObject doorParent;

    // Start is called before the first frame update
    void Start()
    {
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
                doorCount = 36;

            // random door entries (8 options possible).
            if(doorEntries.Count == 0)
            {
                for (int i = 0; i < 8; i++)
                {
                    DoorEntry entry = new DoorEntry();

                    entry.hot = (Random.Range(0, 2) == 1);
                    entry.noisy = (Random.Range(0, 2) == 1);
                    entry.safe = (Random.Range(0, 2) == 1);
                    entry.percent = Random.Range(0.0F, 1.0F);
                }
            }        

        }

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

            // DOOR SETTINGS
            // TODO: add in door settings.

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

            // increase the column count
            colIndex++;

            // at the end of the row, so move onto the next row.
            if(colIndex >= rowLength)
            {
                rowIndex++;
                colIndex = 0;
            }            
        }

        // TODO: option to scale down doors.
        // scale down all doors
        // if(doorParent != null)
        // {
        //     if(doorCount > 128)
        //     {
        // 
        //     }
        //     else if (doorCount > 96)
        //     {
        // 
        //     }
        //     else if (doorCount > 64)
        //     {
        // 
        //     }
        // }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

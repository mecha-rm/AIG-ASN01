using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the gameplay manager.
public class GameplayManager : MonoBehaviour
{
    // the list of doors.
    public List<Door> doors;

    // the prefab for the door.
    public GameObject doorPrefab;

    // spacing.
    public Vector2 spacing = new Vector2(5.0F, 5.0F);

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
        int rowSize = Mathf.CeilToInt(Mathf.Sqrt(doorCount));
        Vector2 doorPosOffset = spacing;

        // generates all of the doors.
        for(int i = 0; i < doorCount; i++)
        {
            GameObject doorObject = Instantiate(doorPrefab);
            Door door;

            // tries to get the door component, adding it if it's not set.
            if(!doorObject.TryGetComponent<Door>(out door))
                door = doorObject.AddComponent<Door>();

            // door.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

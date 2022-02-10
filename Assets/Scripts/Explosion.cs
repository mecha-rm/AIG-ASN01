using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// released by hot door to open up surrounding doors.
public class Explosion : MonoBehaviour
{
    // countdown timer for explosion destroying itself.
    float countdownTimer = 0.0f;

    // the max timer value.
    float timeMax = 1.0F;

    // Start is called before the first frame update
    void Start()
    {
        countdownTimer = timeMax;
    }

    // Update is called once per frame
    void Update()
    {
        countdownTimer -= Time.deltaTime;

        // if time is over, destroy the explosion objct.
        if (countdownTimer <= 0.0F)
            Destroy(gameObject);
    }
}

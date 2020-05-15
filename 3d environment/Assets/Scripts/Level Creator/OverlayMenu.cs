using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayMenu : MonoBehaviour
{
    public Camera targetCamera;
    private int distanceFromCamera = 20;

    private void Update()
    {
        // the only time this menu appears is when the game state is false; therefore paused, no need to update it this isn't the case
        if (!GameState.gameState)
        {
            transform.position = targetCamera.transform.position + (targetCamera.transform.forward * distanceFromCamera);
            transform.LookAt(targetCamera.transform);
            transform.RotateAround(transform.position, transform.up, 180f);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideMouse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
            // Hide the mouse cursor
    Cursor.visible = false;

    // Lock the cursor to the center of the screen
    Cursor.lockState = CursorLockMode.Locked;
    }
}

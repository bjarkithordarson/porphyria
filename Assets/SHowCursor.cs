using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SHowCursor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
            // Hide the mouse cursor
    Cursor.visible = true;

    // Lock the cursor to the center of the screen
    Cursor.lockState = CursorLockMode.None;
    }
}

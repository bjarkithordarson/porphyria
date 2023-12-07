using UnityEngine;

public class CameraTriggerController : MonoBehaviour
{
    public GameObject camera;

    void OnTriggerEnter(Collider other)
    {
        // Check if the character enters the trigger zone
        if (other.CompareTag("Player")) // Ensure your character has the tag "Player"
        {
            CameraHandler.instance.SetActiveCamera(camera);
        }
    }
}

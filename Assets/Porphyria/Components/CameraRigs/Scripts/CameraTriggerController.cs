using UnityEngine;

public class CameraTriggerController : MonoBehaviour
{
    public GameObject camera;

    void OnTriggerStay(Collider other)
    {
        // Check if the character enters the trigger zone
        if (other.CompareTag("Player")) // Ensure your character has the tag "Player"
        {
            CameraHandler.instance.SetActiveCamera(camera);
        }
    }

    private void OnDrawGizmos()
    {
        BoxCollider collider = GetComponent<BoxCollider>();
        if (collider == null)
            return;

        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);

        // Draw opaque yellow box
        Gizmos.color = new Color(1, 1, 0, 0.5f);
        Gizmos.DrawCube(collider.center, collider.size);

        // Draw red wireframe
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(collider.center, collider.size);
    }
}

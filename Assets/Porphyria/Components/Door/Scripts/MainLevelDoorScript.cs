using UnityEngine;

public class MainLevelDoorScript : MonoBehaviour
{
    public Animator leftDoorAnimator;
    public Animator rightDoorAnimator;
    public string openParameterName = "Open"; // The trigger or boolean name in the Animator
    private bool playerIsNear = false;
    public GameObject doorBlocker;
    private AudioSource audioSource;
    public AudioSource audioSourcedoor;

    void Start()
    {
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsNear)
        {
            OpenDoors();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = false;
        }
    }

    private void OpenDoors()
    {
        leftDoorAnimator.enabled = true;
        rightDoorAnimator.enabled = true;
        doorBlocker.SetActive(false);
        audioSource.Play();
    }
}

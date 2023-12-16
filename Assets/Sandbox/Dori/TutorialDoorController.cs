using UnityEngine;
using TMPro;
using JetBrains.Annotations;

public class TutorialDoorController : MonoBehaviour
{
    public Animator leftDoorAnimator;
    public Animator rightDoorAnimator;
    public string openParameterName = "Open"; // The trigger or boolean name in the Animator
    private bool playerIsNear = false;
    public GameObject doorBlocker;
    private AudioSource audioSource;
    public AudioSource audioSourcedoor;

    public StatueRecieverForIntro statueRecieverForIntro;
    public TextMeshPro openDoorText;
    public Collider doorChecker;
    private bool hasOpened = false;
    public Light nextLevelMap;

    void Start()
    {
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsNear && statueRecieverForIntro.hasFinished && !hasOpened )
        {
            OpenDoors();
            openDoorText.gameObject.SetActive(false);
            hasOpened = true;
        }
        if (Input.GetKeyDown(KeyCode.E) && playerIsNear && !hasOpened)
        {
            audioSourcedoor.Play();
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasOpened)
        {
            playerIsNear = true;
            openDoorText.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = false;
            openDoorText.gameObject.SetActive(false);
        }
    }

    private void OpenDoors()
    {
        leftDoorAnimator.enabled = true;
        rightDoorAnimator.enabled = true;
        doorBlocker.SetActive(false);
        audioSource.Play();
        nextLevelMap.intensity = 10;
    }
}

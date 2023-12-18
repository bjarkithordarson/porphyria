using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTriggerController : MonoBehaviour
{
    [SerializeField]
    public AudioClip[] audioClips;
    [Range(0, 1)] public float volume = 0.5f;
    public GameObject audioSpawner;
    [Range(0, 1)]public float chanceOfPlayback = 1;
    public bool playInfinitely = true;
    public int maxNumberOfPlaybacks = 1;
    public int minTimeBetweenPlaybacks = 0;
    private int playbackCounter = 0;

    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = audioSpawner.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!playInfinitely && (maxNumberOfPlaybacks <= playbackCounter))
        {
            return;
        }
        if(Random.Range(0,1) > chanceOfPlayback)
        {
            return;
        }
        if(audioClips.Length > 0 && !audioSource.isPlaying)
        {
            int index = Random.Range(0, audioClips.Length);
            audioSource.clip = audioClips[index];
            audioSource.Play();
            playbackCounter++;
        }
    }
}

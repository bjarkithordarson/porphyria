using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StatueRecieverForIntro : MonoBehaviour
{
public int statuesNeeded;
//private GameManager GameManager;
public TextMeshProUGUI depositText;
public TextMeshProUGUI StatueCount;
public SceneManager SceneManager;
public BoxCollider doorCollider;
public GameObject CounterWeight;

public Animator animator;
// public GameObject trigger;
private BoxCollider boxCollider;
// private ConeDetection ConeDetection;
public GameObject MovingStatue;
public AudioSource audioSource;


private bool canReturnStatues = false;
private bool hasFinished = false;
    void Start()
    {
        // animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (ConeDetection.instance.statueCount < statuesNeeded)
            {   
                depositText.text = "You need 1 statue";
                depositText.enabled = true;
            }
            else
            {

                depositText.text = "Place down statues with 'E'";
                depositText.enabled = true;
            }
        }
        canReturnStatues = ConeDetection.instance.statueCount >= statuesNeeded;
    }

    void OnTriggerExit(Collider other)
    {
        canReturnStatues = false;
        if (other.gameObject.CompareTag("Player"))
        {
            depositText.enabled = false;
        }
    // Start is called before the first frame update
    }
    IEnumerator AllStatuesPlaced()
    {
        yield return new WaitForSeconds(2.5f);
        depositText.text = "All statues placed";
        depositText.enabled = true;
        doorCollider.enabled = true;
        audioSource.Play();
        yield return new WaitForSeconds(2.5f);
        depositText.enabled = false;
        hasFinished = true;


        
    }

    //IEnumerator LoadSecretStudyScreen()
    //{
      //  yield return new WaitForSeconds(2.0f);
    //}

    private void DisableText()
    {
        boxCollider.enabled = false;
        depositText.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canReturnStatues && !hasFinished)
        {
            depositText.text = "Placing statue...";
            depositText.enabled = true;
            CounterWeight.SetActive(true);
            StatueCount.text = "";
            Invoke("DisableText",2.0f);
            animator.enabled = true;
            MovingStatue.SetActive(true);
            
            
            
            
            StartCoroutine(AllStatuesPlaced());
            

            
                      

        }
    }
    }


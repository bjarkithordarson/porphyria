using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StatueRecieverForIntro : MonoBehaviour
{
public int statuesNeeded;
//private GameManager GameManager;
public TextMeshPro depositText;
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
public ConeDetection coneDetection;


private bool canReturnStatues = false;
public bool hasFinished = false;
    void Start()
    {
        // animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (coneDetection.statueCount < statuesNeeded)
            {   
                depositText.text = "You need 1 statue";
                depositText.gameObject.SetActive(true);
            }
            else
            {

                depositText.text = "Place down statue (E)";
                depositText.gameObject.SetActive(true);
            }
        }
        canReturnStatues = coneDetection.statueCount >= statuesNeeded;
    }

    void OnTriggerExit(Collider other)
    {
        canReturnStatues = false;
        if (other.gameObject.CompareTag("Player"))
        {
            depositText.gameObject.SetActive(false);
        }
    // Start is called before the first frame update
    }
    IEnumerator AllStatuesPlaced()
    {
        yield return new WaitForSeconds(2.5f);
        depositText.text = "All statues placed";
        depositText.gameObject.SetActive(true);
        doorCollider.enabled = true;
        audioSource.Play();
        yield return new WaitForSeconds(0.5f);
        depositText.gameObject.SetActive(false);
        hasFinished = true;


        
    }

    //IEnumerator LoadSecretStudyScreen()
    //{
      //  yield return new WaitForSeconds(2.0f);
    //}

    private void DisableText()
    {
        boxCollider.enabled = false;
        depositText.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canReturnStatues && !hasFinished)
        {
            depositText.text = "Placing statue...";
            depositText.gameObject.SetActive(true);
            CounterWeight.SetActive(true);
            StatueCount.text = "";
            Invoke("DisableText",2.0f);
            animator.enabled = true;
            MovingStatue.SetActive(true);
            
            
            
            
            StartCoroutine(AllStatuesPlaced());
            

            
                      

        }
    }
    }


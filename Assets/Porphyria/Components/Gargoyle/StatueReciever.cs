using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StatueReciever : MonoBehaviour
{
public int statuesNeeded;
//private GameManager GameManager;
public TextMeshProUGUI depositText;
public TextMeshProUGUI StatueCount;
public SceneManager SceneManager;
public GameObject Flooring;
public GameObject CounterWeight;
public GameObject Fire;
public Animator animator;
public Animator HatchOpening; 
// public GameObject trigger;
private BoxCollider boxCollider;
// private ConeDetection ConeDetection;


private bool canReturnStatues = false;
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
        // if(GameManager.instance.AmountofStatuesNeeded < GameManager.instance.AmountOfPlacedStatues){
        yield return new WaitForSeconds(2.5f);
        depositText.text = "All statues placed";
        depositText.enabled = true;
        HatchOpening.enabled = true;

        
    }

    IEnumerator LoadSecretStudyScreen()
    {
        yield return new WaitForSeconds(2.0f);
    }

    private void DisableText()
    {
        boxCollider.enabled = false;
        depositText.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canReturnStatues)
        {
            depositText.text = "Placing statue...";
            depositText.enabled = true;
            CounterWeight.SetActive(true);
            Fire.SetActive(true);
            ConeDetection.instance.statueCount--;
            GameManager.instance.AmountOfPlacedStatues++;
            StatueCount.text = "";
            //ConeDetection.instance.ResetStatueCount();
            Invoke("DisableText",2.0f);
            animator.enabled = true;
            AudioManager.instance.StatuePlacementSound();
            
            
            if(GameManager.instance.AmountOfPlacedStatues == GameManager.instance.AmountofStatuesNeeded)
            {
                StartCoroutine(AllStatuesPlaced());

            }
                      

        }
    }
    }


using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StatueReciever : MonoBehaviour
{
public int statuesNeeded;
//private GameManager GameManager;
public TextMeshPro depositText;
public TextMeshProUGUI StatueCount;
public SceneManager SceneManager;
public GameObject Flooring;
public GameObject CounterWeight;
public GameObject Fire;
public Animator animator;
public Animator HatchOpening; 
// public GameObject trigger;
private BoxCollider boxCollider;
public ConeDetection coneDetection;
public FloorOpenEndJuice floorOpenEndJuice;


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
            if (coneDetection.statueCount < statuesNeeded)
            {   
                depositText.text = "You need 1 statue";
                depositText.gameObject.SetActive(true);
            }
            else
            {

                depositText.text = "Place down statues with 'E'";
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
        //if(GameManager.instance.AmountofStatuesNeeded < GameManager.instance.AmountOfPlacedStatues){
        floorOpenEndJuice.ToggleFires();
        yield return new WaitForSeconds(7f);
        //depositText.text = "All statues placed";
        //depositText.gameObject.SetActive(true);
        HatchOpening.enabled = true;
        AudioManager.instance.StoneHatchSound();
        

        
    }

    IEnumerator LoadSecretStudyScreen()
    {
        yield return new WaitForSeconds(2.0f);
    }

    private void DisableText()
    {
        boxCollider.enabled = false;
        depositText.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canReturnStatues)
        {
            depositText.text = "Placing statue...";
            depositText.gameObject.SetActive(true);
            CounterWeight.SetActive(true);
            Fire.SetActive(true);
            coneDetection.statueCount--;
            GameManager.instance.AmountOfPlacedStatues++;
            coneDetection.ResetStatueCount();
            Invoke("DisableText", 1.0f);
            animator.enabled = true;
            AudioManager.instance.StatuePlacementSound();
            
            
            if(GameManager.instance.AmountOfPlacedStatues == GameManager.instance.AmountofStatuesNeeded)
            {
                StartCoroutine(AllStatuesPlaced());

            }
                      

        }
    }
    }


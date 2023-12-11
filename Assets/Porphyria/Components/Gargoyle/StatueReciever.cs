using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StatueReciever : MonoBehaviour
{
public int statuesNeeded;
//private GameManager GameManager;
public TextMeshProUGUI depositText;
public SceneManager SceneManager;
public GameObject Flooring;
public GameObject CounterWeight;
public GameObject Fire;
private Animator Railing_Pillar_Low_01;
// private ConeDetection ConeDetection;

private bool canReturnStatues = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (ConeDetection.instance.statueCount < statuesNeeded)
            {
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
        yield return new WaitForSeconds(3.5f);
        depositText.text = "All statues placed";
        depositText.enabled = true;
        // }
        // Add code to place the statues here
        //StartCoroutine(LoadSecretStudyScreen());
        
    }

    IEnumerator LoadSecretStudyScreen()
    {
        yield return new WaitForSeconds(4.0f);
        SceneManager.LoadScene("AlphaMenu");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canReturnStatues)
        {
            depositText.text = "Placing statue...";
            depositText.enabled = true;
            ConeDetection.instance.statueCount = 0;
            GameManager.instance.AmountOfPlacedStatues++;
            CounterWeight.SetActive(true);
            Fire.SetActive(true);
            Railing_Pillar_Low_01.enabled = true;
            
            if(GameManager.instance.AmountOfPlacedStatues == GameManager.instance.AmountofStatuesNeeded)
            {
                StartCoroutine(AllStatuesPlaced());
            }
                      

        }
    }
    }


using System.Collections;
using TMPro;
using UnityEngine;


public class StatueReciever : MonoBehaviour
{
public int statuesNeeded = 2;
public TextMeshProUGUI depositText;

public GameObject Flooring;

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
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            depositText.enabled = false;
        }
    // Start is called before the first frame update
    }
    IEnumerator PlaceStatues()
    {
        yield return new WaitForSeconds(2.5f);
        depositText.text = "Statues placed";
        depositText.enabled = true;
        // Add code to place the statues here
        Flooring.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        //Debug.Log(ConeDetection.instance.statueCount);
        if(Input.GetKeyDown(KeyCode.E) && ConeDetection.instance.statueCount >= statuesNeeded)
        {
            depositText.text = "Placing statues...";
            depositText.enabled = true;
            StartCoroutine(PlaceStatues());                
            
        }
    }
    }


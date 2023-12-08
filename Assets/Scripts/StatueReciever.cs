using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StatueReciever : MonoBehaviour
{
public int statuesNeeded;
public TextMeshProUGUI depositText;
public SceneManager SceneManager;
public GameObject Flooring;

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
    IEnumerator PlaceStatues()
    {
        yield return new WaitForSeconds(3.5f);
        depositText.text = "Statues placed";
        depositText.enabled = true;
        // Add code to place the statues here
        StartCoroutine(LoadMainScreen());
        depositText.text = "Thanks for playing!";
    }

    IEnumerator LoadMainScreen()
    {
        yield return new WaitForSeconds(4.0f);
        SceneManager.LoadScene("AlphaMenu");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canReturnStatues)
        {
            depositText.text = "Placing statues...";
            depositText.enabled = true;
            StartCoroutine(PlaceStatues());           

        }
    }
    }


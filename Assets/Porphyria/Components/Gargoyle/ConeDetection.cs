using UnityEngine;
using TMPro;

public class ConeDetection : MonoBehaviour
{
    public int statueCount = 0;
    public int requiredStatueCount = 1;
    public TextMeshProUGUI statueCountText;

    private GameObject currentStatue = null;
    public static ConeDetection instance; // Singleton instance

    
    public void ResetStatueCount()
    {
        statueCount = 0;
        statueCountText.text = "";
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && currentStatue != null)
        {
            if(statueCount == 0){

            
            StatueScript statueScript = currentStatue.GetComponent<StatueScript>();
            if (statueScript != null)
            {
                statueCount++;
                statueCountText.text = statueCount.ToString() + "/" + requiredStatueCount;
                statueScript.Interact();
                Destroy(currentStatue);
                currentStatue = null;
            }}
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Statue"))
        {
            currentStatue = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == currentStatue)
        {
            currentStatue = null;
        }
    }
}




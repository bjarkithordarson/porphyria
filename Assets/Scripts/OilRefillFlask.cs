using UnityEngine;

public class FlaskScript : MonoBehaviour
{
    private bool isPlayerNearby = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Make sure your player has the tag "Player"
        {
            isPlayerNearby = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }

    public bool IsPlayerNearby()
    {
        return isPlayerNearby;
    }

    public void ConsumeFlask()
    {
        this.gameObject.SetActive(false);

    }
}

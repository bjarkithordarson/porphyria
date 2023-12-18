using UnityEngine;

public class checkpoint : MonoBehaviour
{
public Transform characterPrefab;
public Transform respawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

void RespawnCharacter()
{   
    Debug.Log("Respawning character...");
    Debug.Log(respawnPoint);
    Debug.Log(characterPrefab);
    characterPrefab.transform.position = respawnPoint.position;
    
}
    // Update is called once per frame
    void Update()
    {
    if(Input.GetKeyDown(KeyCode.C)){
    RespawnCharacter();

    }
    }
}

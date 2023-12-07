using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{
public GameObject characterPrefab;
public Transform respawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

void RespawnCharacter()
{
    Instantiate(characterPrefab, respawnPoint.position, respawnPoint.rotation);
}
    // Update is called once per frame
    void Update()
    {
        
    }
}

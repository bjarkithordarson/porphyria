using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTriggerController : MonoBehaviour
{
    public string sceneName;
    public string colliderTag;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag(colliderTag))
        {
            return;
        }
           
        SceneManager.LoadScene(sceneName);
    }
}

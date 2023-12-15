using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathSceneController : MonoBehaviour
{
    public Animator endScene;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start death scene");
        endScene.enabled = true;
        StartCoroutine(LoadGameScene());
    }


    public IEnumerator LoadGameScene()
    {
        Debug.Log("Before delay");
        yield return new WaitForSeconds(5);
        Debug.Log("After delay");
        Debug.Log(GameManager.instance.gameSceneName);
        SceneManager.LoadScene("AlphaMenu");
    }
}

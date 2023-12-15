using UnityEngine;

public class FloorOpenEndJuice : MonoBehaviour
{
    public GameObject Fire1;
    public GameObject Fire2;
    public GameObject Fire3;
    public GameObject Fire4;
    public GameObject Fire5;
    public GameObject AnimationFire;

    // Define a public method to disable fires and enable AnimationFire
    public void ToggleFires()
    {
        Fire1.SetActive(false);
        Fire2.SetActive(false);
        Fire3.SetActive(false);
        Fire4.SetActive(false);
        Fire5.SetActive(false);
        AnimationFire.SetActive(true);
        AudioManager.instance.FireFloorExplosion();
        Invoke("StopAnimation", 10f);
    }

    public void StopAnimation()
    {
        AnimationFire.SetActive(false);
    }
}

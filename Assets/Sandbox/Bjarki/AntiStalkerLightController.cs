using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiStalkerLightController : MonoBehaviour
{
    public BoxCollider collider;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            collider.enabled = true;
        } else
        {
            collider.enabled = false;
        }
    }
}

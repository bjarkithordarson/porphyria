using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreditsRoll : MonoBehaviour
{
    public float speed = 20f;
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        rectTransform.position += Vector3.up * speed * Time.deltaTime;
    }
}

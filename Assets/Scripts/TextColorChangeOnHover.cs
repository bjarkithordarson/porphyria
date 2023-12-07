using UnityEngine;
using UnityEngine.EventSystems; // Required for the event data
using TMPro; // Required for TextMeshPro elements

public class TextColorChangeOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI buttonText; // Assign this in the inspector
    public Color normalColor = Color.black; // Default color
    public Color hoverColor = Color.red; // Color when hovered

    private void Start()
    {
        if (buttonText == null)
        {
            Debug.LogError("ButtonText not assigned on " + gameObject.name);
        }
        else
        {
            buttonText.color = normalColor; // Set initial color
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.color = hoverColor; // Change text color on hover
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.color = normalColor; // Change text color back to normal when not hovered
    }
}

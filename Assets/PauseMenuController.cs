using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenuController : MonoBehaviour
{
    private UIDocument document;
    VisualElement root;

    public bool isOpen;
    // Start is called before the first frame update
    void Start()
    {
        document = gameObject.GetComponent<UIDocument>();
        root = document.rootVisualElement;

        Close();

        Button ButtonResume = root.Q<Button>("ButtonResume");
        Button ButtonQuit = root.Q<Button>("ButtonQuit");

        ButtonResume.clicked += OnButtonResumeClicked;
        ButtonQuit.clicked += OnButtonQuitClicked;

        Slider SliderMasterVolume = root.Q<Slider>("SliderMasterVolume");
        Slider SliderBrightness = root.Q<Slider>("SliderBrightness");

        SliderMasterVolume.value = GameManager.instance.masterVolume;
        SliderBrightness.value = GameManager.instance.brightness;

        SliderMasterVolume.RegisterValueChangedCallback(OnSliderMasterVolumeChanged);
        SliderBrightness.RegisterValueChangedCallback(OnSliderBrightnessChanged);
    }

    void OnButtonResumeClicked()
    {
        Close();
    }

    void OnButtonQuitClicked()
    {
        Application.Quit();
    }

    void OnSliderMasterVolumeChanged(ChangeEvent<float> e)
    {
        GameManager.instance.masterVolume = e.newValue;
    }
    void OnSliderBrightnessChanged(ChangeEvent<float> e)
    {
        GameManager.instance.brightness = e.newValue;
    }

    void Open()
    {
        root.Q<VisualElement>("MenuWrapper").style.display = DisplayStyle.Flex;
        isOpen = true;
        GameManager.instance.PauseGame();
    }

    void Close()
    {
        root.Q<VisualElement>("MenuWrapper").style.display = DisplayStyle.None;
        isOpen = false;
        GameManager.instance.ResumeGame();
    }

    void Toggle()
    {
        if(isOpen)
        {
            Close();
        } else
        {
            Open();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            Toggle();
        }
    }
}

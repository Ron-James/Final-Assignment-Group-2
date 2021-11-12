using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public static bool OpenSettings;
    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] Slider sensitivitySlider;
    GameObject player;
    float currentSliderValue;
    // Start is called before the first frame update
    void Start()
    {
        OpenSettings = false;
        player = GameObject.FindGameObjectWithTag("Player");
        currentSliderValue = sensitivitySlider.value;
    }

    // Update is called once per frame
    void Update()
    {
        if(sensitivitySlider.value != currentSliderValue && OpenSettings){
            ChangeSens();
        }
    }

    public void ChangeSens(){
        player.GetComponent<FirstPersonAIO>().mouseSensitivity = sensitivitySlider.value;
        currentSliderValue = sensitivitySlider.value;
    }
    public void CloseSettingsMenu(){
        OpenSettings = false;
        pauseMenuUI.SetActive(true);
        settingsMenu.SetActive(false);
    }
    public void OpenSettingMenu(){
        OpenSettings = true;
        if(PauseMenu.IsPaused){
            pauseMenuUI.SetActive(false);
            settingsMenu.SetActive(true);
        }
        else{
            return;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour {
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject GameSettings;
    [SerializeField] GameObject HUD;
    [SerializeField] RocketControls RocketControls;
    [SerializeField] TextMeshProUGUI FuelText;
    [SerializeField] Slider slider;

    void Start() {
        PauseMenu.SetActive(false);
        GameSettings.SetActive(false);
        HUD.SetActive(true);
    }


    void Update() {
        FuelText.text = "Fuel : " + RocketControls.Fuel.ToString("F2");
        slider.value = 500.0f;
        if (Input.GetKey(KeyCode.Escape)) {
            Time.timeScale = 0.0f; //game is paused
            PauseMenu.SetActive(true);
            HUD.SetActive(false );
        }
    }

    public void ResumeGame() {
        Time.timeScale = 1.0f;
        PauseMenu.SetActive(false);
        HUD.SetActive(true);
    }

    public void QuitGameMenu() {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenuScene");
    }

    public void MuteValueChange() {
        //Debug.Log("Mute / UnMute");
        Camera camera = FindFirstObjectByType<Camera>();
        if (camera != null) {
            AudioListener cameraAudio = camera.GetComponent<AudioListener>();
            cameraAudio.enabled = cameraAudio.enabled? false :true;
        }
    }
}

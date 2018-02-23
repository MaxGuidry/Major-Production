using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public AudioClip MusicClip;
    private Canvas MainCanvas;
    public Canvas OptionsCanvas;
    public Slider sensitivity;
    public Toggle invertMouse;
    private AudioSource _backSound;

    private void Start()
    {
        MainCanvas = GetComponent<Canvas>();
        MainCanvas.gameObject.SetActive(true);
        OptionsCanvas.gameObject.SetActive(false);

        _backSound = FindObjectOfType<AudioSource>();
        _backSound.volume = 1;

        sensitivity.value = InputMap.Sensititivity;
        Debug.Log(InputMap.Sensititivity);
        if (InputMap.Sensititivity < 0)
            invertMouse.isOn = true;
        if (MusicClip == null) return;
        _backSound.clip = MusicClip;
        _backSound.Play();
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Options()
    {
        switch (MainCanvas.GetComponentInChildren<Transform>().gameObject.activeInHierarchy)
        {
            case true:
                foreach (var child in MainCanvas.GetComponentsInChildren<Transform>())
                {
                    child.gameObject.SetActive(false);
                }
                OptionsCanvas.gameObject.SetActive(true);
                break;
            case false:
                MainCanvas.gameObject.SetActive(true);
                for (var i = 0; i < MainCanvas.transform.childCount; i++)
                {
                    MainCanvas.transform.GetChild(i).gameObject.SetActive(true);
                    MainCanvas.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
                }
                OptionsCanvas.gameObject.SetActive(false);
                break;
        }
    }

    public void InvertMouse()
    {
        InputMap.Sensititivity *= -1;
    }

    public void MouseSensitivitySlider(float value)
    {
        InputMap.Sensititivity = value;
    }

    public void AudioSlider(float value)
    {
        _backSound.volume = value;
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public AudioClip MusicClip;

    private GameObject BackButtonOptions, OpenStartGameButton, StartGameButton, BackButtonControls;
    private GameObject MainCanvas, ControlsCanvas, OptionsCanvas, StartLocalGameCanvas;
    private AudioSource _backSound;

    private void OnEnable()
    {


        foreach (var child in FindObjectsOfType<Transform>())
        {
            if (child.name.Contains("Main_Canvas"))
            {
                MainCanvas = child.GetComponent<Canvas>().gameObject;
                OpenStartGameButton = MainCanvas.transform.GetChild(1).GetChild(0).GetComponent<Button>().gameObject;
            }
            if (child.name.Contains("Options_Canvas"))
            {
                OptionsCanvas = child.GetComponent<Canvas>().gameObject;
                BackButtonOptions = OptionsCanvas.GetComponentInChildren<Button>().gameObject;
                OptionsCanvas.SetActive(false);
            }
            if (child.name.Contains("Controls_Canvas"))
            {
                ControlsCanvas = child.GetComponent<Canvas>().gameObject;
                BackButtonControls = ControlsCanvas.GetComponentInChildren<Button>().gameObject;
                ControlsCanvas.SetActive(false);
            }
            if (child.name.Contains("Start_Game_Canvas"))
            {
                StartLocalGameCanvas = child.GetComponent<Canvas>().gameObject;
                StartGameButton = StartLocalGameCanvas.transform.GetChild(2).GetChild(3).GetComponent<Button>().gameObject;
                StartLocalGameCanvas.SetActive(false);
            }
        }
    }

    private void Start()
    {
        _backSound = FindObjectOfType<AudioSource>();
        _backSound.volume = .7f;

        if (MusicClip == null) return;
        _backSound.clip = MusicClip;
        _backSound.Play();
        SetSelected();
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void StartLocalGame()
    {
        SwitchCanvas(StartLocalGameCanvas.GetComponent<Canvas>());
        SetSelected();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Options()
    {
        SwitchCanvas(OptionsCanvas.GetComponent<Canvas>());
        SetSelected();
    }

    public void Controls()
    {
        SwitchCanvas(ControlsCanvas.GetComponent<Canvas>());
        SetSelected();
    }

    private void SwitchCanvas(Canvas otherCanvas)
    {
        switch (MainCanvas.GetComponentInChildren<Transform>().gameObject.activeInHierarchy)
        {
            case true:
                MainCanvas.gameObject.SetActive(false);
                otherCanvas.gameObject.SetActive(true);
                break;
            case false:
                MainCanvas.gameObject.SetActive(true);
                otherCanvas.gameObject.SetActive(false);
                break;
        }
    }

    private void SetSelected()
    {
        if (BackButtonOptions.activeInHierarchy)
        {
            EventSystem.current.SetSelectedGameObject(BackButtonOptions);
        }
        else if (OpenStartGameButton.activeInHierarchy)
        {
            EventSystem.current.SetSelectedGameObject(OpenStartGameButton);
        }
        else if (StartGameButton.activeInHierarchy)
        {
            EventSystem.current.SetSelectedGameObject(StartGameButton);
        }
        else if (BackButtonControls.activeInHierarchy)
        {
            EventSystem.current.SetSelectedGameObject(BackButtonControls);
        }
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public AudioClip MusicClip;
    private GameObject GameTimerSlider, EndScreenTimerSlider;
    private GameObject GameTime, EndTimer;
    private GameObject BackButtonOptions, OpenStartGameButton, StartGameButton, BackButtonControls;
    private GameObject MainCanvas, ControlsCanvas, OptionsCanvas, StartLocalGameCanvas;
    private AudioSource _backSound;

    private void Awake()
    {
        foreach (var child in FindObjectsOfType<Transform>())
        {
            if (child.name.Contains("Main_Canvas"))
            {
                MainCanvas = child.GetComponent<Canvas>().gameObject;
                OpenStartGameButton = MainCanvas.transform.GetChild(0).GetChild(0).GetComponent<Button>().gameObject;
            }
            if (child.name.Contains("Options_Canvas"))
            {
                OptionsCanvas = child.GetComponent<Canvas>().gameObject;
                BackButtonOptions = OptionsCanvas.GetComponentInChildren<Button>().gameObject;
                foreach (var childOptions in OptionsCanvas.GetComponentsInChildren<Transform>())
                {
                    if (childOptions.name.Contains("Game_Time"))
                    {
                        GameTimerSlider = childOptions.GetComponentInChildren<Slider>().gameObject;
                        GameTime = childOptions.GetComponentInChildren<Text>().gameObject;
                    }
                    if (childOptions.name.Contains("End_Time"))
                    {
                        EndScreenTimerSlider = childOptions.GetComponentInChildren<Slider>().gameObject;
                        EndTimer = childOptions.GetComponentInChildren<Text>().gameObject;
                    }
                }
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
        var GSlider = GameTimerSlider.GetComponent<Slider>();
        GSlider.maxValue = 999;
        GSlider.minValue = 30;
        GSlider.value = 90;
        var ESlider = EndScreenTimerSlider.GetComponent<Slider>();
        ESlider.maxValue = 10;
        ESlider.minValue = 1;
        ESlider.value = 10;

        Config.EditSettings.RoundTime = GameTimerSlider.GetComponent<Slider>().value;
        Config.EditSettings.EndScreenTimer = EndScreenTimerSlider.GetComponent<Slider>().value;
        Config.SaveSettings();

        GameTime.GetComponent<Text>().text = "Game Time: " + Mathf.Round(Config.EditSettings.RoundTime);
        EndTimer.GetComponent<Text>().text = "End Screen Time: " + Mathf.Round(Config.EditSettings.EndScreenTimer);

        _backSound = FindObjectOfType<AudioSource>();
        _backSound.volume = 1;

        if (MusicClip == null) return;
        _backSound.clip = MusicClip;
        _backSound.Play();
        SetSelected();
    }

    public void Update()
    {
        Config.EditSettings.RoundTime = GameTimerSlider.GetComponent<Slider>().value;
        Config.EditSettings.EndScreenTimer = EndScreenTimerSlider.GetComponent<Slider>().value;
        GameTime.GetComponent<Text>().text = "Game Time: " + Mathf.Round(Config.EditSettings.RoundTime);
        EndTimer.GetComponent<Text>().text = "End Screen Time: " + Mathf.Round(Config.EditSettings.EndScreenTimer);
        Config.SaveSettings();
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
                foreach (var child in MainCanvas.GetComponentsInChildren<Transform>())
                {
                    child.gameObject.SetActive(false);
                }
                otherCanvas.gameObject.SetActive(true);
                break;
            case false:
                MainCanvas.gameObject.SetActive(true);
                for (var i = 0; i < MainCanvas.transform.childCount; i++)
                {
                    if (MainCanvas.transform.GetChild(0))
                    {
                        for (var x = 0; x < MainCanvas.transform.GetChild(0).childCount; x++)
                        {
                            MainCanvas.transform.GetChild(0).GetChild(x).gameObject.SetActive(true);
                            MainCanvas.transform.GetChild(0).GetChild(x).GetChild(0).gameObject.SetActive(true);
                        }
                    }
                    MainCanvas.transform.GetChild(i).gameObject.SetActive(true);
                    MainCanvas.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
                }
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

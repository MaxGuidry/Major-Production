using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public AudioClip MusicClip;
    public Canvas OptionsCanvas, StartLocalGameCanvas;
    public Slider GameTimerSlider;
    public Slider EndScreenTimer;
    public Text GameTime;
    public Text EndTimer;
    public GameObject BackButton, StartButton, FourPlayerStart;
    public Canvas MainCanvas;
    private AudioSource _backSound;

    private void Start()
    {
        GameTimerSlider.maxValue = 999;
        GameTimerSlider.minValue = 30;
        GameTimerSlider.value = 90;
        EndScreenTimer.maxValue = 10;
        EndScreenTimer.minValue = 1;
        EndScreenTimer.value = 10;
        Config.EditSettings.RoundTime = GameTimerSlider.value;
        Config.EditSettings.EndScreenTimer = EndScreenTimer.value;
        Config.SaveSettings();
        GameTime.text = "Game Time: " + Mathf.Round(Config.EditSettings.RoundTime);
        EndTimer.text = "End Screen Time: " + Mathf.Round(Config.EditSettings.EndScreenTimer);
        MainCanvas.gameObject.SetActive(true);
        OptionsCanvas.gameObject.SetActive(false);
        StartLocalGameCanvas.gameObject.SetActive(false);

        _backSound = FindObjectOfType<AudioSource>();
        _backSound.volume = 1;

        if (MusicClip == null) return;
        _backSound.clip = MusicClip;
        _backSound.Play();
        SetSelected();
    }

    public void Update()
    {
        Config.EditSettings.RoundTime = GameTimerSlider.value;
        Config.EditSettings.EndScreenTimer = EndScreenTimer.value;
        GameTime.text = "Game Time: " + Mathf.Round(Config.EditSettings.RoundTime);
        EndTimer.text = "End Screen Time: " + Mathf.Round(Config.EditSettings.EndScreenTimer);
        Config.SaveSettings();
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void StartLocalGame()
    {
        SwitchCanvas(StartLocalGameCanvas);
        SetSelected();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Options()
    {
        SwitchCanvas(OptionsCanvas);
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
                    MainCanvas.transform.GetChild(i).gameObject.SetActive(true);
                    MainCanvas.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
                }
                otherCanvas.gameObject.SetActive(false);
                break;
        }
    }

    private void SetSelected()
    {
        if (BackButton.activeInHierarchy)
        {    
            EventSystem.current.SetSelectedGameObject(BackButton);
        }
        else if (StartButton.activeInHierarchy)
        {
            EventSystem.current.SetSelectedGameObject(StartButton);
        }
        else if (FourPlayerStart.activeInHierarchy)
        {
            EventSystem.current.SetSelectedGameObject(FourPlayerStart);
        }
    }
}

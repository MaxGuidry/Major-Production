using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuBehaviour : MonoBehaviour
{
    public Slider GameTime, EndTime;
    public Toggle FullScreenToggle;
    public Dropdown ResolutionDrop;
    public Dropdown TextureDrop;
    public Dropdown AntiDrop;
    public Dropdown VSyncDrop;
    public Button ApplyButton;
    public Button RevertButton;
    public List<Resolution> Resolutions;
    public Text GameTimeText;
    public Text EndTimeText;
 
    void Start()
    {
        //SETUP ui callbacks
        GameTime.onValueChanged.AddListener(delegate { OnUIGameTimeChange(); });
        EndTime.onValueChanged.AddListener(delegate { OnUIEndTimeChange(); });
        FullScreenToggle.onValueChanged.AddListener(delegate { OnUIFullScreenToggle(); });
        ResolutionDrop.onValueChanged.AddListener(delegate { OnUIResolutionChange(); });
        TextureDrop.onValueChanged.AddListener(delegate { OnUITextureQualityChange(); });
        AntiDrop.onValueChanged.AddListener(delegate { OnUIAntiChange(); });
        VSyncDrop.onValueChanged.AddListener(delegate { OnUIVSyncChange(); });
        ApplyButton.onClick.AddListener(delegate { SettingManager.Instance.SaveSettings(); });
        RevertButton.onClick.AddListener(delegate { SettingManager.Instance.LoadSettings(); });

        Resolutions = Screen.resolutions.ToList();

        foreach (var res in Resolutions)
        {
            ResolutionDrop.options.Add(new Dropdown.OptionData(res.ToString()));
        }

        SettingManager.Instance.ON_LOADSETTINGS.AddListener(OnSettingsChanged);
        SettingManager.Instance.ON_SETTINGSCHANGED.AddListener(OnSettingsChanged);
    }

    public void ForceRefresh()
    {
        var config = SettingManager.Instance.Config;
        FullScreenToggle.isOn = config.FullScreen;
        ResolutionDrop.value = config.ResolutionIndex;
        VSyncDrop.value = config.VSync;
        GameTime.value = config.GameTime;
        EndTime.value = config.EndTime;
        AntiDrop.value = config.AntiAliasing;
        TextureDrop.value = config.TextureQuality;

        GameTimeText.text = "Game Time: " + Mathf.Round(SettingManager.Instance.GameTime) * 10;
        EndTimeText.text = "End Screen Time: " + Mathf.Round(SettingManager.Instance.EndTime);
    }
    public void OnSettingsChanged(GameSettingsConfig config)
    {
        //updating ui
        FullScreenToggle.isOn = config.FullScreen;
        ResolutionDrop.value = config.ResolutionIndex;
        VSyncDrop.value = config.VSync;
        GameTime.value = config.GameTime;
        EndTime.value = config.EndTime;
        AntiDrop.value = config.AntiAliasing;
        TextureDrop.value = config.TextureQuality;
    }

    public void OnUIFullScreenToggle()
    {
        SettingManager.Instance.FullScreen = Screen.fullScreen = FullScreenToggle.isOn;
    }

    public void OnUIResolutionChange()
    {
        Screen.SetResolution(Resolutions[ResolutionDrop.value].width, Resolutions[ResolutionDrop.value].height, Screen.fullScreen);
        SettingManager.Instance.ResolutionIndex = ResolutionDrop.value;
    }

    public void OnUITextureQualityChange()
    {
        QualitySettings.masterTextureLimit = SettingManager.Instance.TextureQuality = TextureDrop.value;
    }

    public void OnUIAntiChange()
    {
        QualitySettings.antiAliasing = (int)Mathf.Pow(2, AntiDrop.value);
        SettingManager.Instance.AntiAliasing = AntiDrop.value;
    }

    public void OnUIVSyncChange()
    {
        QualitySettings.vSyncCount = SettingManager.Instance.VSync = VSyncDrop.value;
    }

    public void OnUIGameTimeChange()
    {
        SettingManager.Instance.GameTime = (int)GameTime.value;
        GameTimeText.text = "Game Time: " + Mathf.Round(SettingManager.Instance.GameTime) * 10;
    }

    public void OnUIEndTimeChange()
    {
        SettingManager.Instance.EndTime = (int)EndTime.value;
        EndTimeText.text = "End Screen Time: " + Mathf.Round(SettingManager.Instance.EndTime);
    }
}

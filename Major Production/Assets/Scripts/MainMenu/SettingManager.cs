using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class SettingManager : MonoBehaviour
{
    public Toggle FullScreenToggle;
    public Dropdown ResolutionDrop, TextureDrop, AntiDrop, VSyncDrop;
    public List<Resolution> resolutions;
    public Button ApplyButton;
    public Button RevertButton;
    private GameSettingsManager GameSettings;

    private void OnEnable()
    {
        GameSettings = new GameSettingsManager();
        FullScreenToggle.onValueChanged.AddListener(delegate { OnFullScreenToggle(); });
        ResolutionDrop.onValueChanged.AddListener(delegate { OnResoultionChange(); });
        TextureDrop.onValueChanged.AddListener(delegate { OnTextureQualityChange(); });
        AntiDrop.onValueChanged.AddListener(delegate { OnAntiChange(); });
        VSyncDrop.onValueChanged.AddListener(delegate { OnVSyncChange(); });
        ApplyButton.onClick.AddListener(delegate { SaveSettings(); });
        RevertButton.onClick.AddListener(delegate { LoadSettings(); });
        resolutions = Screen.resolutions.ToList();

        foreach (var res in resolutions)
        {
            ResolutionDrop.options.Add(new Dropdown.OptionData(res.ToString()));
        }
        LoadSettings();
    }

    public void OnFullScreenToggle()
    {
        GameSettings.FullScreen = Screen.fullScreen = FullScreenToggle.isOn;
    }

    public void OnResoultionChange()
    {
        Screen.SetResolution(resolutions[ResolutionDrop.value].width, resolutions[ResolutionDrop.value].height, Screen.fullScreen);
        GameSettings.ResolutionIndex = ResolutionDrop.value;
    }

    public void OnTextureQualityChange()
    {
        QualitySettings.masterTextureLimit = GameSettings.TextureQuality = TextureDrop.value;
    }

    public void OnAntiChange()
    {
        QualitySettings.antiAliasing = (int)Mathf.Pow(2, AntiDrop.value);
        GameSettings.AntiAliasing = AntiDrop.value;
    }

    public void OnVSyncChange()
    {
        QualitySettings.vSyncCount = GameSettings.VSync = VSyncDrop.value;
    }

    public void SaveSettings()
    {
        var data = JsonUtility.ToJson(GameSettings, true);
        File.WriteAllText(Application.persistentDataPath + "/gameSettings.json", data);
    }

    public void LoadSettings()
    {
        if (File.Exists("gameSettings.json"))
        {
            var data = File.ReadAllText(Application.persistentDataPath + "/gameSettings.json");
            GameSettings = JsonUtility.FromJson<GameSettingsManager>(data);

            FullScreenToggle.isOn = GameSettings.FullScreen;
            AntiDrop.value = GameSettings.AntiAliasing;
            VSyncDrop.value = GameSettings.VSync;
            TextureDrop.value = GameSettings.TextureQuality;
            ResolutionDrop.value = GameSettings.ResolutionIndex;
        }
    }
}

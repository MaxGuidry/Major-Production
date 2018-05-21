using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    public Toggle FullScreenToggle;
    public Dropdown ResolutionDrop, TextureDrop, AntiDrop, VSyncDrop;
    public Resolution[] resolutions;
    public GameSettingsBehaviour GameSettings;

    private void OnEnable()
    {
        GameSettings = new GameSettingsBehaviour();
        FullScreenToggle.onValueChanged.AddListener(delegate { OnFullScreenToggle(); });
        ResolutionDrop.onValueChanged.AddListener(delegate { OnResoultionChange(); });
        TextureDrop.onValueChanged.AddListener(delegate { OnTextureQualityChange(); });
        AntiDrop.onValueChanged.AddListener(delegate { OnAntiChange(); });
        VSyncDrop.onValueChanged.AddListener(delegate { OnVSyncChange(); });
        resolutions = Screen.resolutions;
    }

    public void OnFullScreenToggle()
    {
        GameSettings.FullScreen = Screen.fullScreen = FullScreenToggle.isOn;
    }

    public void OnResoultionChange()
    {

    }

    public void OnTextureQualityChange()
    {
        QualitySettings.masterTextureLimit = GameSettings.TextureQuality = TextureDrop.value;
    }

    public void OnAntiChange()
    {

    }

    public void OnVSyncChange()
    {

    }
}

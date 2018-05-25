using System.IO;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class SettingManager : ScriptableObject
{
    public SaveLoadEvent ON_SAVESETTINGS = new SaveLoadEvent();
    public SaveLoadEvent ON_LOADSETTINGS = new SaveLoadEvent();
    public SaveLoadEvent ON_SETTINGSCHANGED = new SaveLoadEvent();
    private static SettingManager _instance;

    [SerializeField]
    private GameSettingsConfig _config;

    public GameSettingsConfig Config
    {
        get { return _config; }
    }
    public static SettingManager Instance
    {
        get
        {
            if (!_instance)
                _instance = Resources.FindObjectsOfTypeAll<SettingManager>().FirstOrDefault();
            if (!_instance)
                _instance = Resources.Load<SettingManager>("_Settings");
            return _instance;
        }
    }


    private string path
    {
        get { return Application.persistentDataPath + "/gameSettings.json"; }
    }

    public int GameTime
    {
        get { return _config.GameTime; }
        set
        {
            _config.GameTime = value;
            ON_SETTINGSCHANGED.Invoke(_config);
        }
    }

    public bool FullScreen
    {
        get { return _config.FullScreen; }
        set
        {
            _config.FullScreen = value;
            ON_SETTINGSCHANGED.Invoke(_config);

        }
    }

    public int TextureQuality
    {
        get { return _config.TextureQuality; }
        set
        {
            _config.TextureQuality = value;
            ON_SETTINGSCHANGED.Invoke(_config);
        }
    }

    public int AntiAliasing
    {
        get { return _config.AntiAliasing; }
        set
        {
            _config.AntiAliasing = value;
            ON_SETTINGSCHANGED.Invoke(_config);
        }
    }

    public int VSync
    {
        get { return _config.VSync; }
        set
        {
            _config.VSync = value;
            ON_SETTINGSCHANGED.Invoke(_config);
        }
    }

    public int ResolutionIndex
    {
        get { return _config.ResolutionIndex; }
        set
        {
            _config.ResolutionIndex = value;
            ON_SETTINGSCHANGED.Invoke(_config);
        }
    }

    public int EndTime
    {
        get { return _config.EndTime; }
        set
        {
            _config.EndTime = value;
            ON_SETTINGSCHANGED.Invoke(_config);
        }
    }

    private void OnEnable()
    {
        _config = new GameSettingsConfig();
        
        if (!LoadSettings()) //if file doesn't exist
        {
            _config.GameTime = 3;
            _config.AntiAliasing = 3;
            _config.EndTime = 10;
            _config.FullScreen = true;
            _config.ResolutionIndex = 0;
            _config.TextureQuality = 0;
            _config.VSync = 1;
            SaveSettings(); //make it
        }
    }

    public bool LoadSettings()
    {
        if (!File.Exists(path))
        {
            Debug.LogError("no file");
            return false;
        }
            
        var data = File.ReadAllText(path);
        JsonUtility.FromJsonOverwrite(data, _config);
        ON_LOADSETTINGS.Invoke(_config);
        return true;
    }

    public void SaveSettings()
    {
        var data = JsonUtility.ToJson(_config, true);
        File.WriteAllText(path, data);
        ON_SAVESETTINGS.Invoke(_config);
    }

    private void OnDisable()
    {
        SaveSettings();
    }
}
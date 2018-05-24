using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Config : MonoBehaviour
{
    public static ConfigSettings EditSettings = new ConfigSettings();
    public static FileStream fileStream;
    #region References

    public CountDown Countdown;


    #endregion

    private bool open = false;
    void OnEnable()
    {

        
        string path = Application.dataPath + "/bin/config.json";
        if (!File.Exists(path))
        {
            if (!Directory.Exists(Application.dataPath + "/bin"))
                Directory.CreateDirectory(Application.dataPath + "/bin");

            fileStream = File.Create(path);
            fileStream.Close();


            ConfigSettings config = new ConfigSettings();
            config.EndScreenTimer = 3f;
            config.RoundTime = 60f;
            config.DontYouDareTouchThisVariable__Thanks = "Stop it.";
            File.WriteAllText(path, JsonUtility.ToJson(config));
        }
        else
        {
            fileStream = File.Open(path, FileMode.Open);
            fileStream.Close();
        }
        if (!Countdown)
            return;
        if (!LoadSettings())
            StartCoroutine(TrySaveSettingsIfFail());
    }

    private IEnumerator TrySaveSettingsIfFail()
    {
        float timer = 2f;
        while (!LoadSettings() && timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
    }
    [System.Serializable]
    public class ConfigSettings
    {
        public ConfigSettings()
        {
            DontYouDareTouchThisVariable__Thanks = "";
            RoundTime = 60f;
            EndScreenTimer = 10f;
        }
        public string DontYouDareTouchThisVariable__Thanks;
        public float RoundTime;
        public float EndScreenTimer;
    }

    public bool LoadSettings()
    {

        string path = Application.dataPath + "/bin/config.json";

        if (!File.Exists(path))
        {
            if (!Directory.Exists(Application.dataPath + "/bin"))
                Directory.CreateDirectory(Application.dataPath + "/bin");
            fileStream = File.Create(path);
            fileStream.Close();
            
        }

        if (!File.Exists(path))
        {
            return false;
        }

        fileStream= File.OpenRead(path);
        string configJSON = File.ReadAllText(path);
        ConfigSettings config = JsonUtility.FromJson<ConfigSettings>(configJSON);
        if (config == null)
        {
            Debug.LogError("Config object null");
            return false;
        }

        if (Countdown == null)
        {
            Debug.LogError("Countdown object null");
            return false;

        }
        Countdown.Timer = config.RoundTime;
        Countdown.GameOverScreenTimer = config.EndScreenTimer;
        fileStream.Close();
        return true;
    }

    public static void SaveSettings()
    {
        string path = Application.dataPath + "/bin/config.json";

        if (!File.Exists(path))
        {
            if (!Directory.Exists(Application.dataPath + "/bin"))
                Directory.CreateDirectory(Application.dataPath + "/bin");
           fileStream= File.Create(path);
           fileStream.Close();
            if (!File.Exists(path))
            {
                return;
            }
        }

        if (fileStream == null)
            return;
        if (!fileStream.CanRead)
            return;
        fileStream = File.Open(path,FileMode.Open);
        if (!fileStream.CanWrite)
            return;
        string json = JsonUtility.ToJson(EditSettings);        
        File.WriteAllText(path, json);
        fileStream.Close();
    }
}

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Config : MonoBehaviour
{

    #region References

    public CountDown Countdown;


    #endregion

    void Awake()
    {
        if (!Countdown)
            return;
        string path = Application.dataPath + "/bin/config.json";
        if (!File.Exists(path))
        {
            if (!Directory.Exists(Application.dataPath + "/bin"))
                Directory.CreateDirectory(Application.dataPath + "/bin");

            var filestream = File.Create(path);
            filestream.Close();


            ConfigSettings config = new ConfigSettings();
            config.EndScreenTimer = 3f;
            config.RoundTime = 60f;
            File.WriteAllText(path, JsonUtility.ToJson(config));
        }

        LoadSettings();
    }
    public class ConfigSettings
    {
        public float RoundTime;
        public float EndScreenTimer;
    }

    public void LoadSettings()
    {
        string path = Application.dataPath + "/bin/config.json";

        if (!File.Exists(path))
        {
            if (!Directory.Exists(Application.dataPath + "/bin"))
                Directory.CreateDirectory(Application.dataPath + "/bin");
            File.Create(path);
        }

        if (!File.Exists(path))
        {
            return;
        }
        string configJSON = File.ReadAllText(path);
        ConfigSettings config = JsonUtility.FromJson<ConfigSettings>(configJSON);
        Countdown.Timer = config.RoundTime;
        Countdown.GameOverScreenTimer = config.EndScreenTimer;
    }

}

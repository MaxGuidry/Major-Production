using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class AutoTyper : MonoBehaviour
{
    public float TypeSpeed;
    public Text TextArea;
    public GameObject BackgroundPanel;
    public string ChooseFile;

    private string path;
    // Use this for initialization
    void Start()
    {
        path = "Assets/Resources/" + ChooseFile + ".txt";
        StartCoroutine(AutoType());
    }

    private void SetActive(bool Bool)
    {
        BackgroundPanel.gameObject.SetActive(Bool);
        TextArea.gameObject.SetActive(Bool);
    }

    public IEnumerator AutoType()
    {
        SetActive(true);
        if (ChooseFile == "")
        {
            SetActive(false);
            yield break;
        }
        StreamReader reader;
        if (File.Exists(path))
            reader = new StreamReader(path);
        else
        {
            SetActive(false);
            yield break;
        }
        while (true)
        {
            if (!reader.EndOfStream)
            {
                foreach (var letter in reader.ReadLine())
                {
                    TextArea.text += letter;
                    yield return new WaitForSeconds(TypeSpeed);
                }
                TextArea.text = "";
            }
            else
            {
                SetActive(false);
                yield break;
            }
        }
    }
}

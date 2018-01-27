using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class AutoTyper : MonoBehaviour
{
    public float TypeSpeed;
    public Text TestText;
    public GameObject TestPanel;
    public string ChooseFile;

    private string path;
    // Use this for initialization
    void Start ()
    {
        path = "Assets/Resources/" + ChooseFile + ".txt";
        StartCoroutine(AutoType());
	}

    private IEnumerator AutoType()
    {
        TestPanel.gameObject.SetActive(true);
        TestText.gameObject.SetActive(true);

        if (ChooseFile == "")
        {
            TestPanel.gameObject.SetActive(false);
            TestText.gameObject.SetActive(false);
            yield break;
        }

        StreamReader reader;
        if(File.Exists(path))
            reader = new StreamReader(path);
        else
        {
            TestPanel.gameObject.SetActive(false);
            TestText.gameObject.SetActive(false);
            yield break;
        }
        while (true)
        {
            if (!reader.EndOfStream)
            {
                foreach (var letter in reader.ReadLine())
                {
                    TestText.text += letter;
                    yield return new WaitForSeconds(TypeSpeed);
                }
                TestText.text = "";
            }
            else
            {
                TestPanel.gameObject.SetActive(false);
                TestText.gameObject.SetActive(false);
                yield break;
            }
        }
    }
}

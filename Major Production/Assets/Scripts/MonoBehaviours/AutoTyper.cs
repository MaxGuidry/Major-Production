using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class AutoTyper : MonoBehaviour
{
    public GameObject BackgroundPanel;
    public string ChooseFile;

    [HideInInspector] public string path;

    public Text TextArea;

    public float TypeSpeed;

    // Use this for initialization
    private void Start()
    {
        path = "Assets/Resources/Dialogue/" + ChooseFile + ".txt";
        StartCoroutine(AutoType());
    }

    private void SetActive(bool Bool)
    {
        BackgroundPanel.gameObject.SetActive(Bool);
        TextArea.gameObject.SetActive(Bool);
    }

    public IEnumerator AutoType()
    {
        SetActive(!BackgroundPanel.gameObject.activeInHierarchy);
        if (ChooseFile == "")
        {
            SetActive(false);
            yield break;
        }

        StreamReader reader;
        if (File.Exists(path))
        {
            reader = new StreamReader(path);
        }
        else
        {
            SetActive(false);
            yield break;
        }

        while (true)
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
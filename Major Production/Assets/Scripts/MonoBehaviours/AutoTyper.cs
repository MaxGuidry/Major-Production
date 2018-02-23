using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class AutoTyper : MonoBehaviour
{
    public GameObject BackgroundPanel, Target;
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
        var startPos = BackgroundPanel.transform.position;
        var back = false;
        SetActive(true);
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
                BackgroundPanel.transform.position = Vector3.Lerp(BackgroundPanel.transform.position,
                    !back ? Target.gameObject.transform.position : startPos, Time.deltaTime);

                if (Vector3.Distance(BackgroundPanel.transform.position,
                        Target.transform.position) <= 2)
                    back = true;
                foreach (var letter in reader.ReadLine())
                {
                    TextArea.text += letter;
                    yield return new WaitForSeconds(TypeSpeed);
                }

                TextArea.text = "";
                if (back && Vector3.Distance(BackgroundPanel.transform.position,
                        startPos) <= 2)
                    back = false;
            }
            else
            {

                SetActive(false);
                yield break;
            }
    }
}
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
        var canvasGroup = GetComponent<CanvasGroup>();
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
                {
                    FadeIn(canvasGroup);
                    foreach (var letter in reader.ReadLine())
                    {
                        TextArea.text += letter;
                        yield return new WaitForSeconds(TypeSpeed);
                    }

                    TextArea.text = "";
                }
            }
            else
            {
                FadeOut(canvasGroup);
                SetActive(false);
                yield return null;
            }
    }

    public void FadeIn(CanvasGroup cg)
    {
        StartCoroutine(FadeCanvasGroup(cg, cg.alpha, 1, 1));
    }

    public void FadeOut(CanvasGroup cg)
    {
        StartCoroutine(FadeCanvasGroup(cg, cg.alpha, 0, 1));
    }

    public IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float lerpTime)
    {
        var startLerp = Time.time;
        var timesinceStart = Time.time - startLerp;
        var percentageComple = timesinceStart / lerpTime;

        while (true)
        {
            timesinceStart = Time.time - startLerp;
            percentageComple = timesinceStart / lerpTime;

            var currentValue = Mathf.Lerp(start, end, percentageComple);

            cg.alpha = currentValue;

            if (percentageComple >= 1) break;

            yield return new WaitForEndOfFrame();
        }
    }
}
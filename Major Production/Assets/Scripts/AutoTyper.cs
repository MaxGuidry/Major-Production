using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoTyper : MonoBehaviour
{
    public float TypeSpeed;
    public Text TestText;
    public GameObject TestPanel;
    public List<string> Dialogue;
    // Use this for initialization
    void Start ()
    {
        StartCoroutine(AutoType());
	}

    private IEnumerator AutoType()
    {
        var i = 0;
        while (true)
        {
            if (i < Dialogue.Count)
            {
                foreach (var letter in Dialogue[i])
                {
                    TestText.text += letter;
                    yield return new WaitForSeconds(TypeSpeed);
                }
                TestText.text = "";
                i++;
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

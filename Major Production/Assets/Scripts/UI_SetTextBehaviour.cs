using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

public class UI_SetTextBehaviour : MonoBehaviour
{
    private Text txt;
    void Start()
    {
        txt = GetComponent<Text>();
    }
    public void UpdateText(Object[] args)
    {
        var sender = args[0] as Quest;
        if (sender != null)
        {
            txt.text = "Current Quest:" + sender.Description + "<color=green>" + sender.CurrentAmount + " / " +
                       sender.RequiredAmount + "</color>";
        }
    }
}

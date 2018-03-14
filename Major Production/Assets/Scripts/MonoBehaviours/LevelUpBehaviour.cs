using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpBehaviour : MonoBehaviour
{
    public GameObject panel;

    public void ShowUI()
    {
        panel.SetActive(!panel.activeInHierarchy);
    }

    public void UpgradeStat(object[] args)
    {
        if (args.Length < 2)
            return;
        if (args[1] as string == "DPad Horizontal")
        {
            Debug.Log("Horizontal");
            ShowUI();
        }
        if (args[1] as string == "DPad Vertical")
        {
            Debug.Log("Vertical");
            ShowUI();
        }
    }
}

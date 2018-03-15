using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpBehaviour : MonoBehaviour
{
    public GameObject panel;
    private PlayerStatBehaviour playerStat;

    void Start()
    {
        playerStat = FindObjectOfType<PlayerStatBehaviour>();
    }

    /// <summary>
    ///     Checks if the panel is active and sets it accordlying
    /// </summary>
    public void ShowUI()
    {
        panel.SetActive(!panel.activeInHierarchy);
    }

    /// <summary>
    ///     Used to start the coroutine
    /// </summary>
    public void StartUpgrade()
    {
        StartCoroutine(UpgradeStat());
    }

    /// <summary>
    ///     Runs until a stat is upgraded
    /// </summary>
    /// <returns></returns>
    private IEnumerator UpgradeStat()
    {
        var StatUpGraded = false;
        while (!StatUpGraded)
        {
            if (Input.GetAxis("DPad Horizontal") == -1)
            {
                playerStat.stats.GetStat("PHealth").Value++;
                ShowUI();
                StatUpGraded = true;
            }

            if (Input.GetAxis("DPad Horizontal") == 1)
            {
                playerStat.stats.GetStat("PDamage").Value++;
                ShowUI();
                StatUpGraded = true;
            }

            if (Input.GetAxis("DPad Vertical") == 1)
            {
                playerStat.stats.GetStat("PArmor").Value++;
                ShowUI();
                StatUpGraded = true;
            }

            if (Input.GetAxis("DPad Vertical") == -1)
            {
                playerStat.stats.GetStat("PSpeed").Value++;
                ShowUI();
                StatUpGraded = true;
            }
            yield return null;
        }
    }
}

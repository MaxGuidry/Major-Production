using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpBehaviour : MonoBehaviour
{
    public GameObject panel;
    private PlayerStatBehaviour playerStat;
    private bool statUpGraded;

    void Start()
    {
        playerStat = FindObjectOfType<PlayerStatBehaviour>();
        statUpGraded = false;
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
        statUpGraded = false;
        StartCoroutine(UpgradeStat());
    }

    /// <summary>
    ///     Runs until a stat is upgraded
    /// </summary>
    /// <returns></returns>
    private IEnumerator UpgradeStat()
    {
        while (!statUpGraded)
        {
            if (Input.GetAxis("DPad Horizontal") == -1 || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                UpgradeStatsProcess("PHealth");
            }

            if (Input.GetAxis("DPad Horizontal") == 1 || Input.GetKeyDown(KeyCode.RightArrow))
            {
                UpgradeStatsProcess("PDamage");
            }

            if (Input.GetAxis("DPad Vertical") == 1 || Input.GetKeyDown(KeyCode.UpArrow))
            {
                UpgradeStatsProcess("PArmor");
            }

            if (Input.GetAxis("DPad Vertical") == -1 || Input.GetKeyDown(KeyCode.DownArrow))
            {
                UpgradeStatsProcess("PSpeed");
            }
            yield return null;
        }
    }

    /// <summary>
    ///     Used to remove redundancy
    /// </summary>
    /// <param name="statName"></param>
    private void UpgradeStatsProcess(string statName)
    {
        playerStat.stats.GetStat(statName).Value += 10;
        ShowUI();
        statUpGraded = true;
    }
}

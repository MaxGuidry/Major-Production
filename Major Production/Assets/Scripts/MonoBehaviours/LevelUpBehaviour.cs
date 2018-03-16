using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpBehaviour : MonoBehaviour
{
    public GameObject panel;
    private PlayerStatBehaviour playerStat;
    private int UpgradePoint;
    void Start()
    {
        playerStat = FindObjectOfType<PlayerStatBehaviour>();
    }

    void Update()
    {
        if (UpgradePoint > 0)
            panel.SetActive(true);
        else
        {
            panel.SetActive(false);
        }
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
        UpgradePoint++;
        StartCoroutine(UpgradeStat());
    }

    /// <summary>
    ///     Runs until a stat is upgraded
    /// </summary>
    /// <returns></returns>
    private IEnumerator UpgradeStat()
    {
        var statUpGraded = false;
        while (!statUpGraded)
        {
            if (Input.GetAxis("DPad Horizontal") == -1 || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                statUpGraded = true;
                StartCoroutine(DoubleClicked("PHealth"));
            }

            if (Input.GetAxis("DPad Horizontal") == 1 || Input.GetKeyDown(KeyCode.RightArrow))
            {
                statUpGraded = true;
                StartCoroutine(DoubleClicked("PDamage"));
            }

            if (Input.GetAxis("DPad Vertical") == 1 || Input.GetKeyDown(KeyCode.UpArrow))
            {
                statUpGraded = true;
                StartCoroutine(DoubleClicked("PArmor"));
            }

            if (Input.GetAxis("DPad Vertical") == -1 || Input.GetKeyDown(KeyCode.DownArrow))
            {
                statUpGraded = true;
                StartCoroutine(DoubleClicked("PSpeed"));
            }
            yield return null;
        }
    }

    private IEnumerator DoubleClicked(string statName)
    {
        var doubleClicked = false;
        yield return new WaitForSeconds(.2f);
        while (!doubleClicked)
        {
            var haxis = Input.GetAxis("DPad Horizontal");
            var vaxis = Input.GetAxis("DPad Vertical");

            switch (statName)
            {
                case "PHealth":
                    if (Input.GetAxis("DPad Horizontal") == -1 || Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        doubleClicked = true;
                        UpgradeStatsProcess("PHealth");
                        UpgradePoint--;
                    }
                    else if (haxis != 0 || vaxis != 0)
                    {
                        doubleClicked = true;
                        StartUpgrade();
                    }
                    break;

                case "PDamage":
                    if (Input.GetAxis("DPad Horizontal") == 1 || Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        doubleClicked = true;
                        UpgradeStatsProcess("PDamage");
                        UpgradePoint--;
                    }
                    else if (haxis != 0 || vaxis != 0)
                    {
                        doubleClicked = true;
                        StartUpgrade();
                    }
                    break;

                case "PArmor":
                    if (Input.GetAxis("DPad Vertical") == 1 || Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        doubleClicked = true;
                        UpgradeStatsProcess("PArmor");
                        UpgradePoint--;
                    }
                    else if (haxis != 0 || vaxis != 0)
                    {
                        doubleClicked = true;
                        StartUpgrade();
                    }
                    break;

                case "PSpeed":
                    if (Input.GetAxis("DPad Vertical") == -1 || Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        doubleClicked = true;
                        UpgradeStatsProcess("PSpeed");
                        UpgradePoint--;
                    }
                    else if (haxis != 0 || vaxis != 0)
                    {
                        doubleClicked = true;
                        StartUpgrade();
                    }
                    break;
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
    }
}

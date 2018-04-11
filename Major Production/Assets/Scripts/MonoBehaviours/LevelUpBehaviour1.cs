using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpBehaviour1 : MonoBehaviour
{
    public GameObject panel;
    private PlayerStatBehaviour playerStat;
    void Start()
    {
        playerStat = gameObject.GetComponent<PlayerStatBehaviour>();
       
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
        var statUpGraded = false;
        while (!statUpGraded)
        {
            if (Input.GetAxis("DPad Horizontal 1") == -1 || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                statUpGraded = true;
                StartCoroutine(DoubleClicked("PHealth 1"));
            }

            if (Input.GetAxis("DPad Horizontal 1") == 1 || Input.GetKeyDown(KeyCode.RightArrow))
            {
                statUpGraded = true;
                StartCoroutine(DoubleClicked("PDamage 1"));
            }

            if (Input.GetAxis("DPad Vertical 1") == 1 || Input.GetKeyDown(KeyCode.UpArrow))
            {
                statUpGraded = true;
                StartCoroutine(DoubleClicked("PArmor 1"));
            }

            if (Input.GetAxis("DPad Vertical 1") == -1 || Input.GetKeyDown(KeyCode.DownArrow))
            {
                statUpGraded = true;
                StartCoroutine(DoubleClicked("PSpeed 1"));
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
            var haxis = Input.GetAxis("DPad Horizontal 1");
            var vaxis = Input.GetAxis("DPad Vertical 1");

            switch (statName)
            {
                case "PHealth 1":
                    if (Input.GetAxis("DPad Horizontal 1") == -1 || Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        doubleClicked = true;
                        UpgradeStatsProcess("PHealth 1");
                    }
                    else if (haxis != 0 || vaxis != 0)
                    {
                        doubleClicked = true;
                        StartUpgrade();
                    }
                    break;

                case "PDamage 1":
                    if (Input.GetAxis("DPad Horizontal 1") == 1 || Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        doubleClicked = true;
                        UpgradeStatsProcess("PDamage 1");
                    }
                    else if (haxis != 0 || vaxis != 0)
                    {
                        doubleClicked = true;
                        StartUpgrade();
                    }
                    break;

                case "PArmor 1":
                    if (Input.GetAxis("DPad Vertical 1") == 1 || Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        doubleClicked = true;
                        UpgradeStatsProcess("PArmor 1");
                    }
                    else if (haxis != 0 || vaxis != 0)
                    {
                        doubleClicked = true;
                        StartUpgrade();
                    }
                    break;

                case "PSpeed 1":
                    if (Input.GetAxis("DPad Vertical 1") == -1 || Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        doubleClicked = true;
                        UpgradeStatsProcess("PSpeed 1");
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

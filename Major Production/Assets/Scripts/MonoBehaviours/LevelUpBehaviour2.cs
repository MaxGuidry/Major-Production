using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpBehaviour2 : MonoBehaviour
{
    public GameObject panel;
    private PlayerStatBehaviour playerStat;
    private int UpgradePoint;
    void Start()
    {
        playerStat = gameObject.GetComponent<PlayerStatBehaviour>();
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
            if (Input.GetAxis("DPad Horizontal2") == -1 || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                statUpGraded = true;
                StartCoroutine(DoubleClicked("PHealth 2"));
            }

            if (Input.GetAxis("DPad Horizontal2") == 1 || Input.GetKeyDown(KeyCode.RightArrow))
            {
                statUpGraded = true;
                StartCoroutine(DoubleClicked("PDamage 2"));
            }

            if (Input.GetAxis("DPad Vertical2") == 1 || Input.GetKeyDown(KeyCode.UpArrow))
            {
                statUpGraded = true;
                StartCoroutine(DoubleClicked("PArmor 2"));
            }

            if (Input.GetAxis("DPad Vertical2") == -1 || Input.GetKeyDown(KeyCode.DownArrow))
            {
                statUpGraded = true;
                StartCoroutine(DoubleClicked("PSpeed 2"));
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
            var haxis = Input.GetAxis("DPad Horizontal2");
            var vaxis = Input.GetAxis("DPad Vertical2");

            switch (statName)
            {
                case "PHealth 2":
                    if (Input.GetAxis("DPad Horizontal2") == -1 || Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        doubleClicked = true;
                        UpgradeStatsProcess("PHealth 2");
                        UpgradePoint--;
                    }
                    else if (haxis != 0 || vaxis != 0)
                    {
                        doubleClicked = true;
                        StartUpgrade();
                    }
                    break;

                case "PDamage 2":
                    if (Input.GetAxis("DPad Horizontal2") == 1 || Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        doubleClicked = true;
                        UpgradeStatsProcess("PDamage 2");
                        UpgradePoint--;
                    }
                    else if (haxis != 0 || vaxis != 0)
                    {
                        doubleClicked = true;
                        StartUpgrade();
                    }
                    break;

                case "PArmor 2":
                    if (Input.GetAxis("DPad Vertical2") == 1 || Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        doubleClicked = true;
                        UpgradeStatsProcess("PArmor 2");
                        UpgradePoint--;
                    }
                    else if (haxis != 0 || vaxis != 0)
                    {
                        doubleClicked = true;
                        StartUpgrade();
                    }
                    break;

                case "PSpeed 2":
                    if (Input.GetAxis("DPad Vertical2") == -1 || Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        doubleClicked = true;
                        UpgradeStatsProcess("PSpeed 2");
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// ReSharper disable CompareOfFloatsByEqualityOperator

public class LevelUpBehaviour : MonoBehaviour
{
    public GameObject panel;
    private PlayerStatBehaviour playerStat;
    private string playerNumber;
    private List<TweenScaleBehaviour> tweens = new List<TweenScaleBehaviour>();
    void Start()
    {
        playerStat = gameObject.GetComponent<PlayerStatBehaviour>();
        switch (gameObject.transform.tag)
        {
            case "P1":
                playerNumber = "";
                break;
            case "P2":
                playerNumber = " 1";
                break;
            case "P3":
                playerNumber = " 2";
                break;
            case "P4":
                playerNumber = " 3";
                break;
            default:
                break;
        }

    }

    /// <summary>
    ///     Used to start the coroutine
    /// </summary>
    public void StartUpgrade()
    {
        var children = gameObject.transform.parent.GetComponentInChildren<Canvas>().gameObject.GetComponentsInChildren<Text>();
        foreach (var stat in children)
        {
            if (stat.GetComponent<TweenScaleBehaviour>())
                tweens.Add(stat.GetComponent<TweenScaleBehaviour>());
        }
        foreach (var tween in tweens)
        {
            tween.tweened = tween.gameObject;
        }

        if (panel != null)
        {
            panel.GetComponent<TweenScaleBehaviour>().TweenScale();
            StartCoroutine(UpgradeStat());
        }
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
            //    if (Input.GetAxis("DPad Horizontal" + playerNumber) == -1 || Input.GetKeyDown(KeyCode.LeftArrow))
            //    {
            //        statUpGraded = true;
            //        StartCoroutine(DoubleClicked("PHealth" + playerNumber));
            //        panel.GetComponent<Image>().sprite = Left;
            //    }

            //    if (Input.GetAxis("DPad Horizontal" + playerNumber) == 1 || Input.GetKeyDown(KeyCode.RightArrow))
            //    {
            //        statUpGraded = true;
            //        StartCoroutine(DoubleClicked("PDamage" + playerNumber));
            //        panel.GetComponent<Image>().sprite = Right;
            //    }

            //    if (Input.GetAxis("DPad Vertical" + playerNumber) == 1 || Input.GetKeyDown(KeyCode.UpArrow))
            //    {
            //        statUpGraded = true;
            //        StartCoroutine(DoubleClicked("PArmor" + playerNumber));
            //        panel.GetComponent<Image>().sprite = Up;
            //    }

            //    if (Input.GetAxis("DPad Vertical" + playerNumber) == -1 || Input.GetKeyDown(KeyCode.DownArrow))
            //    {
            //        statUpGraded = true;
            //        StartCoroutine(DoubleClicked("PSpeed" + playerNumber));
            //        panel.GetComponent<Image>().sprite = Down;
            //    }


            statUpGraded = true;
            UpgradeStatsProcess("PHealth" + playerNumber);
            UpgradeStatsProcess("PDamage" + playerNumber);
            UpgradeStatsProcess("PArmor" + playerNumber);
            UpgradeStatsProcess("PSpeed" + playerNumber);
            yield return null;
        }
    }

    private IEnumerator DoubleClicked(string statName)
    {
        var doubleClicked = false;
        yield return new WaitForSeconds(.2f);
        while (!doubleClicked)
        {
            var haxis = Input.GetAxis("DPad Horizontal" + playerNumber);
            var vaxis = Input.GetAxis("DPad Vertical" + playerNumber);

            if (statName == "PHealth" + playerNumber)
            {
                if (Input.GetAxis("DPad Horizontal" + playerNumber) == -1 || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    doubleClicked = true;
                    UpgradeStatsProcess("PHealth" + playerNumber);
                }
                else if (haxis != 0 || vaxis != 0)
                {
                    doubleClicked = true;
                    StartUpgrade();
                }
            }
            else if (statName == "PDamage" + playerNumber)
            {
                if (Input.GetAxis("DPad Horizontal" + playerNumber) == 1 || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    doubleClicked = true;
                    UpgradeStatsProcess("PDamage" + playerNumber);
                }
                else if (haxis != 0 || vaxis != 0)
                {
                    doubleClicked = true;
                    StartUpgrade();
                }
            }
            else if (statName == "PArmor" + playerNumber)
            {
                if (Input.GetAxis("DPad Vertical" + playerNumber) == 1 || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    doubleClicked = true;
                    UpgradeStatsProcess("PArmor" + playerNumber);
                }
                else if (haxis != 0 || vaxis != 0)
                {
                    doubleClicked = true;
                    StartUpgrade();
                }
            }
            else if (statName == "PSpeed" + playerNumber)
            {
                if (Input.GetAxis("DPad Vertical" + playerNumber) == -1 || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    doubleClicked = true;
                    UpgradeStatsProcess("PSpeed" + playerNumber);
                }
                else if (haxis != 0 || vaxis != 0)
                {
                    doubleClicked = true;
                    StartUpgrade();
                }
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
        playerStat.stats.GetStat(statName).Value += 20;
        foreach (var stat in tweens)
        {
            if (stat.gameObject.GetComponent<Text>().text.Contains(statName))
                stat.TweenScale();
        }
       // panel.GetComponent<Image>().sprite = Default;
    }
}

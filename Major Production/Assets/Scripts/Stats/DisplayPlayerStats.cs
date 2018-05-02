using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPlayerStats : MonoBehaviour
{
    [SerializeField] private bool created;

    public PlayerStatBehaviour PlayerStats;

    [SerializeField] private List<Text> TempText;

    public Text TextPrefab;

    // Use this for initialization
    private void Start()
    {
        PlayerStats = gameObject.transform.parent.parent.GetComponentInChildren<PlayerStatBehaviour>();
        TempText = new List<Text>();
        foreach (var child in gameObject.transform.GetComponentsInChildren<Text>())
        {
            TempText.Add(child);
        }
    }

    private void Update()
    {
        if (PlayerStats == null)
            PlayerStats = gameObject.transform.parent.parent.GetComponentInChildren<PlayerStatBehaviour>();
        foreach (var text in TempText)
        {
            text.fontSize = 12;
            if (text.name.Contains("Health"))
            {
                text.text = PlayerStats.GetComponent<PlayerStatBehaviour>().Health.ToString();
            }
            else if (text.name.Contains("Armor"))
            {
                text.text = PlayerStats.GetComponent<PlayerStatBehaviour>().Armor.ToString();
            }
            else if (text.name.Contains("Speed"))
            {
                text.text = PlayerStats.GetComponent<PlayerStatBehaviour>().Speed.ToString();
            }
            else if (text.name.Contains("Damage"))
            {
                text.text = PlayerStats.GetComponent<PlayerStatBehaviour>().Damage.ToString();
            }
            else if (text.name.Contains("Level"))
            {
                text.text = "Level: " +  PlayerStats.GetComponent<PlayerStatBehaviour>().Level.ToString();
            }
            else if (text.name.Contains("EXP"))
            {
                text.text = "EXP: " + PlayerStats.GetComponent<PlayerStatBehaviour>().EXP.ToString();
            }
        }
        //for (var i = 0; i < PlayerStats.stats._stats.Count; i++)
        //{
        //    TempText[i].text = PlayerStats.stats._stats[i].Name + ": " + PlayerStats.stats._stats[i].Value;
        //    TempText[i].fontSize = 12;

        //    if (PlayerStats.stats._stats[i].Name.Contains("Health"))
        //    {
        //        TempText[i].text = PlayerStats.GetComponent<PlayerStatBehaviour>().Health.ToString();
        //    }
        //    else if (PlayerStats.stats._stats[i].Name.Contains("Armor"))
        //    {
        //        TempText[i].text = PlayerStats.GetComponent<PlayerStatBehaviour>().Armor.ToString();
        //    }
        //    else if (PlayerStats.stats._stats[i].Name.Contains("Speed"))
        //    {
        //        TempText[i].text = PlayerStats.GetComponent<PlayerStatBehaviour>().Speed.ToString();
        //    }
        //    else if (PlayerStats.stats._stats[i].Name.Contains("Damage"))
        //    {
        //        TempText[i].text = PlayerStats.GetComponent<PlayerStatBehaviour>().Damage.ToString();
        //    }
        //}
    }

    //public void CreateText()
    //{
    //    PlayerStats = gameObject.transform.parent.parent.GetComponentInChildren<PlayerStatBehaviour>();
    //    var textPos = gameObject.transform.position;
    //    if (PlayerStats == null)
    //        return;
    //    if (PlayerStats.stats == null)
    //        return;
    //    if (!created)
    //        for (var i = 0; i < PlayerStats.stats._stats.Count; i++)
    //        {
    //            var temp = Instantiate(TextPrefab, textPos, transform.rotation);
    //            temp.transform.LookAt(gameObject.transform.parent.parent.GetComponentInChildren<Camera>().transform);
    //            TempText.Add(temp);

    //            temp.transform.SetParent(gameObject.transform);
    //            temp.text = PlayerStats.stats._stats[i].Name + ": " + PlayerStats.stats._stats[i].Value;
    //            created = true;
    //        }
    //    else
    //    {
    //        for (var i = 0; i < PlayerStats.stats._stats.Count; i++)
    //            TempText[i].text = PlayerStats.stats._stats[i].Name + ": " + PlayerStats.stats._stats[i].Value;
    //    }
    //}
}
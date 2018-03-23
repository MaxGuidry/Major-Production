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
    }

    private void Update()
    {
        CreateText();
    }

    public void CreateText()
    {
        var textPos = gameObject.transform.position;
        if (PlayerStats == null)
            return;
        if (PlayerStats.stats == null)
            return;
        if (!created)
            for (var i = 0; i < PlayerStats.stats._stats.Count; i++)
            {
                var temp = Instantiate(TextPrefab, textPos, transform.rotation);
                temp.transform.LookAt(gameObject.transform.parent.parent.GetComponentInChildren<Camera>().transform);
                TempText.Add(temp);

                temp.transform.SetParent(gameObject.transform);
                temp.text = PlayerStats.stats._stats[i].Name + ": " + PlayerStats.stats._stats[i].Value;
                created = true;
            }
        else
        {
            for (var i = 0; i < PlayerStats.stats._stats.Count; i++)
                TempText[i].text = PlayerStats.stats._stats[i].Name + ": " + PlayerStats.stats._stats[i].Value;
        }
    }
}
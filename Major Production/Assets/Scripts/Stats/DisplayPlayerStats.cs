using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DisplayPlayerStats : MonoBehaviour
{
    public PlayerStatBehaviour PlayerStats;
    public Text TextPrefab;
    // Use this for initialization
    void Start()
    {
        PlayerStats = FindObjectOfType<PlayerStatBehaviour>();
        CreateText();
    }
    void CreateText()
    {
        var textPos = Vector3.zero;
        var temp = TextPrefab;
        for (var i = 0; i < PlayerStats.stats._stats.Count; i++)
        {
            temp = Instantiate(TextPrefab, textPos, transform.rotation) as Text;
            temp.transform.SetParent(gameObject.transform);
        }

        foreach (var stat in PlayerStats.stats._stats)
        {
            temp.text = stat.Name + " " +stat.Value.ToString();
        }
    }
}

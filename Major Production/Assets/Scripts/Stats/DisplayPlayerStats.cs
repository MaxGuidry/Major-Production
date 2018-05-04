using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPlayerStats : MonoBehaviour
{
    public Font DefaultFont;
    public Slider ExpSlider;
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

        ExpSlider.value = 0;
    }

    private void Update()
    {
        if (PlayerStats == null)
            PlayerStats = gameObject.transform.parent.parent.GetComponentInChildren<PlayerStatBehaviour>();
        foreach (var text in TempText)
        {
            text.font = DefaultFont;
            text.fontSize = 10;
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
                text.text = "Level: " + PlayerStats.GetComponent<PlayerStatBehaviour>().Level.ToString();
            }
            else if (text.name.Contains("EXP"))
            {
                ExpSlider.value = PlayerStats.GetComponent<PlayerStatBehaviour>().EXP;
                text.text = "EXP: " + PlayerStats.GetComponent<PlayerStatBehaviour>().EXP.ToString();
            }
        }
    }
}
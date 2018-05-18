using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPlayerStats : MonoBehaviour
{
    public Font DefaultFont;
    public Slider ExpSlider, HealthSlider;
    public PlayerStatBehaviour PlayerStats;
    [SerializeField] private List<Text> TempText;
    public Text TextPrefab;
    private bool maxChanged;
    public List<Sprite> Sprites;
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
                var health = PlayerStats.GetComponent<PlayerStatBehaviour>().Health;
                if (!maxChanged)
                {
                    if (health <= 100)
                    {
                        HealthSlider.maxValue = 100;
                    }
                    else
                    {
                        HealthSlider.maxValue = 200;
                        maxChanged = true;
                    }
                }

                HealthSlider.value = health;
                text.text = "";               
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
                text.text = "";
                foreach (var child in text.GetComponentsInChildren<Image>())
                {
                    switch (child.name)
                    {
                        case "NUM":
                            switch (PlayerStats.GetComponent<PlayerStatBehaviour>().Level)
                            {
                                case 1:
                                    child.GetComponent<Image>().sprite = Sprites[1];
                                    break;
                                case 2:
                                    child.GetComponent<Image>().sprite = Sprites[2];
                                    break;
                                case 3:
                                    child.GetComponent<Image>().sprite = Sprites[3];
                                    break;
                                case 4:
                                    child.GetComponent<Image>().sprite = Sprites[4];
                                    break;
                                case 5:
                                    child.GetComponent<Image>().sprite = Sprites[5];
                                    break;
                                case 6:
                                    child.GetComponent<Image>().sprite = Sprites[6];
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "LVL":
                            child.GetComponent<Image>().sprite = Sprites[0];
                            break;
                    }
                }
            }
            else if (text.name.Contains("EXP"))
            {
                ExpSlider.value = PlayerStats.GetComponent<PlayerStatBehaviour>().EXP;
                text.text = "";
            }
        }
    }
}
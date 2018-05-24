using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsTextBehaviour : MonoBehaviour {
    public Font TextFont;
    public Color TextColor;
    public bool BestFit;
    public int FontSize;
    public Sprite ButtonSprite;
    public Color Normal, Highlighted, Pressed;
	// Update is called once per frame
	void Update () {
        foreach (var button in FindObjectsOfType<Button>())
        {
            var cb = button.colors;
            cb.normalColor = Normal;
            cb.highlightedColor = Highlighted;
            cb.pressedColor = Pressed;
            button.colors = cb;
            var image = button.GetComponent<Image>();
            image.sprite = ButtonSprite;
            var text = button.GetComponentInChildren<Text>();
            if (text == null)
                return;
            text.font = TextFont;
            text.color = TextColor;
            text.fontSize = FontSize;
        }
    }
}

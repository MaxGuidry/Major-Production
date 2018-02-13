using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SliderLabel : MonoBehaviour
{

    public Text text;
    public string Label;

    void Start()
    {
        Label = text.text;
        UpdateLabel();
        
    }
    public void UpdateLabel()
    {
        if (Label == "")
            return;
        text.text = Label + (int)GetComponent<Slider>().value;
    }
}

using System.Collections;
using System.Collections.Generic;
using Cloth;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
public class SliderBehavior : MonoBehaviour
{
    //public List<Slider> sliders = new List<Slider>();
    public Slider slider;
    public Object objectToChange;
    private bool start;
    void Start()
    {
        start = true;
        slider.onValueChanged.Invoke(0);
    }
   
    public void ChangeValue(string s)
    {
        
            
        
        //List<string> vars = new List<string>();
        Dictionary<string, FieldInfo> vars = new Dictionary<string, FieldInfo>();
        List<FieldInfo> fields = getType(objectToChange).GetFields().ToList();
        foreach (var fieldInfo in fields)
        {
            vars.Add(fieldInfo.Name, fieldInfo);
        }

        if(start)
        {
            start = false;
            slider.value = (float)vars[s].GetValue(objectToChange);
            return;
        }
        vars[s].SetValue(objectToChange, slider.value);
    }
    public System.Type getType(object o)
    {
        return o.GetType();
    }
}

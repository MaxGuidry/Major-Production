using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Stat {

    public int BaseValue;
    [SerializeField]
    private List<int> _modifiers = new List<int>();

    public int GetValue()
    {
        var finalValue = BaseValue;
        _modifiers.ForEach(x => finalValue += x);
        return finalValue;
    }
    public void AddModifier(int modifier)
    {
        if (modifier != 0)
            _modifiers.Add(modifier);
    }

    public void RemoveModifier(int modifier)
    {
        if (modifier != 0)
            _modifiers.Remove(modifier);
    }
}

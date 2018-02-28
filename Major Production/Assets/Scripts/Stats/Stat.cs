using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Stat
{
    public int BaseValue;
    [SerializeField]
    private List<int> _modifiers = new List<int>();

    /// <summary>
    /// Returns the final value accounting for every mod in modifiers 
    /// </summary>
    /// <returns></returns>
    public int GetValue()
    {
        var finalValue = BaseValue;
        _modifiers.ForEach(x => finalValue += x);
        return finalValue;
    }

    /// <summary>
    /// Add a modifier to the list
    /// </summary>
    /// <param name="modifier"></param>
    public void AddModifier(int modifier)
    {
        _modifiers.Add(modifier);
    }

    /// <summary>
    /// Check if List is not empty and remove modifier
    /// </summary>
    /// <param name="modifier"></param>
    public void RemoveModifier(int modifier)
    {
        if (modifier != 0)
            _modifiers.Remove(modifier);
    }

    /// <summary>
    /// Clear Modifiers List
    /// </summary>
    public void ClearModifiers()
    {
        _modifiers.Clear();
    }
}

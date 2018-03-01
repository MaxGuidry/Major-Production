using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Stat : ScriptableObject
{
    [SerializeField] private int _baseValue;
    public string Name;
    public int Value;
    public Dictionary<int, Modifier> Modifiers = new Dictionary<int, Modifier>();

    public void OnEnable()
    {
        Value = _baseValue;
    }

    public string AddMod(int id, Modifier mod)
    {
        Modifiers.Add(id, mod);
        var result = string.Format("Add Mod: {0} {1} {2}", Modifiers[id].Target,
            Modifiers[id].Type,
            Modifiers[id].Value);
        return result;
    }

    public string RemoveMod(int id)
    {
        Modifiers.Remove(id);
        var result = string.Format("Remove Mod: {0} {1} {2}", Modifiers[id].Target.name,
            Modifiers[id].Type, Modifiers[id].Value);
        return result;
    }

    public void ClearModifiers()
    {
        Modifiers.Clear();
    }

    public void ApplyMod(Modifier mod)
    {
        switch (mod.Type)
        {
            case ModType.Add:
                Value += mod.Value;
                break;
            case ModType.Mult:
                Value += _baseValue * mod.Value / 10;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void RemoveMod(Modifier mod)
    {
        switch (mod.Type)
        {
            case ModType.Add:
                Value -= mod.Value;
                break;
            case ModType.Mult:
                Value -= _baseValue * mod.Value / 10;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    ///// <summary>
    ///// Returns the final value accounting for every mod in modifiers 
    ///// </summary>
    ///// <returns></returns>
    //public int GetValue()
    //{
    //    var finalValue = BaseValue;
    //    _modifiers.ForEach(x => finalValue += x);
    //    return finalValue;
    //}

    ///// <summary>
    ///// Add a modifier to the list
    ///// </summary>
    ///// <param name="modifier"></param>
    //public void AddModifier(int modifier)
    //{
    //    _modifiers.Add(modifier);
    //}

    ///// <summary>
    ///// Check if List is not empty and remove modifier
    ///// </summary>
    ///// <param name="modifier"></param>
    //public void RemoveModifier(int modifier)
    //{
    //    if (modifier != 0)
    //        _modifiers.Remove(modifier);
    //}

    ///// <summary>
    ///// Clear Modifiers List
    ///// </summary>
    //public void ClearModifiers()
    //{
    //    _modifiers.Clear();
    //}
}
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Stat : ScriptableObject
{
#if UNITY_EDITOR
    [SerializeField]
#endif
    private int _baseValue;
    public string Name;
    public int Value;
    public Dictionary<Modifier, Modifier> Modifiers = new Dictionary<Modifier, Modifier>();

    public void OnEnable()
    {
        Value = _baseValue;
        this.Name = this.name;
    }

    public bool AddMod(Modifier mod)
    {
        if (Modifiers.ContainsKey(mod))
            return false;
        Modifiers.Add(mod, mod);
        return true;
    }

    public bool RemoveMod(Modifier mod)
    {
        if (Modifiers.ContainsKey(mod))
        {
            Modifiers.Remove(mod);
            return true;
        }

        return false;
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

    public void UnApplyMod(Modifier mod)
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
}
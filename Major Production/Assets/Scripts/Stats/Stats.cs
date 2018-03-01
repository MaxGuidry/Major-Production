using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class Stats : ScriptableObject
{
    /// <summary>
    /// stats to be assigned in the inspector
    /// </summary>
    public List<Stat> _stats;
    /// <summary>
    /// the dictionary of stats built from inspector
    /// </summary>
    private Dictionary<string, Stat> _statsDict;

    /// <summary>
    /// initialize the dictionary
    /// </summary>
    public void OnEnable()
    {
        if (_stats != null)
        {
            _statsDict = new Dictionary<string, Stat>();
            _stats.ForEach(s => _statsDict.Add(s.Name, s));
        }
    }

    /// <summary>
    /// get the stat from this configuration by name
    /// </summary>
    /// <param name="statName">string value of name to get, this is by default the name of the scriptable object</param>
    /// <returns></returns>
    public Stat GetStat(string statName)
    {
        Stat result;
        _statsDict.TryGetValue(statName, out result);
        return result;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
[CreateAssetMenu(fileName = "EntityStats", menuName = "EntityStats")]
public class EntityStats : ScriptableObject
{
    public List<float> NeedsList;
    public Dictionary<string, float> NeedsDictionary;
    public bool Alive;
    void OnEnable()
    {
        NeedsDictionary = new Dictionary<string, float>();
        foreach (var need in NeedsList)
        {
            NeedsDictionary.Add("Need", need);
        }
    }

    public float GetNeed(string value)
    {
        return NeedsDictionary[value];
    }
}

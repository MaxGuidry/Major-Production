using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

public class PlayerQuestTrackerBehaviour : MonoBehaviour
{
    public List<Quest> CurrentQuests;
    public void AddQuest(Object[] args)
    {
        var sender = args[0] as Quest;
        if (sender == null)
            return;
        CurrentQuests.Add(sender);
    }
}

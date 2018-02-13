using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjects;
[CreateAssetMenu]
public class Objective : ScriptableObject
{
    public enum ObjectiveType
    {
        None = 0,
        Gather = 1,
        Reach = 2
    }

    public enum ObjectiveStatus
    {
        None = 0,
        Active = 1,
        Complete = 2
    }

    public enum ActionOnReach
    {
        MarkComplete = 0,
        AddModififer = 1,
        PlayAudio = 2,
    }

    public string Title;
    [Multiline]
    public string Description;
    public ObjectiveType MissionType;
    public Item RequiredItem;
    public int CurrentAmount = 0;
    public int RequiredAmount = 5;
    public ObjectiveStatus Status;
    public GameObject Target;
    public Objective NextObjective;
    public List<ActionOnReach> ActionsOnReach;
    public Stat Mod;
    public Objectives ParentScript { get; set; }

    public GameEventArgs QuestStarted;
    public GameEventArgs QuestEnded;
    public GameEventArgs QuestChange;

    public void OnReach(Objective CurrentObj)
    {
        if (CurrentObj.ActionsOnReach.Contains(ActionOnReach.MarkComplete))
            CurrentObj.Status = ObjectiveStatus.Complete;
    }  
}

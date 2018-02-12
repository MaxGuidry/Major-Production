using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public ObjectiveStatus Status;
    public GameObject Target;
    public Objective NextObjective;
    public List<ActionOnReach> ActionsOnReach;
    public Stat Mod;
    public Objectives ParentScript { get; set; }
    public void OnReach()
    {
        if (this.ActionsOnReach.Contains(ActionOnReach.MarkComplete))
            Status = ObjectiveStatus.Complete;
    }
}

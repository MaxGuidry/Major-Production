using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjects;
public enum ObjectiveType
{
    None = 0,
    Gather = 1,
    Reach = 2
}

public enum ObjectiveStatus
{
    None = 0,
    Inactive = 1,
    Active = 2,
    Complete = 3
}

public enum ActionOnReach
{
    MarkComplete = 0,
    AddModififer = 1,
    PlayAudio = 2,
    PlayAnimation = 3,
}
[CreateAssetMenu]
public class Objective : ScriptableObject
{
    public string Title;
    [Multiline]
    public string Description;
    public ObjectiveType MissionType;
    public Item RequiredItem;
    public int CurrentAmount = 0;
    public int RequiredAmount = 5;
    private ObjectiveStatus _status;
    public ObjectiveStatus Status
    {
        get { return _status; }
        set { _status = value; }
    }
    public GameObject Target;
    public GameObject PlayerForStat;
    public GameEventArgs QuestStarted;
    public GameEventArgs QuestEnded;
    public GameEventArgs QuestChange;
    public UnityEngine.Events.UnityEvent actionsOnReach;
    public void MarkComplete()
    {
        _status = ObjectiveStatus.Complete;
    }
    public void OnReach(Objective CurrentObj)
    {
        CurrentObj.actionsOnReach.Invoke();
    }
}

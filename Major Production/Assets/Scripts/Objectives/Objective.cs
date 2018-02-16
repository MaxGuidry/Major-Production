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

    [SerializeField]
    private string Title;
    [Multiline]
    
    [SerializeField]    
    private string Description;
    public string description
    {
        get { return Description; }
    }

    [SerializeField]
    public ObjectiveType MissionType;
    [SerializeField]
    private Item RequiredItem;
    [SerializeField]
    private int CurrentAmount = 0;
    public int currentAmount
    {
        get { return CurrentAmount; }
    }
    [SerializeField]
    private int RequiredAmount = 5;
    public int requiredAmount
    {
        get { return RequiredAmount; }
    }

    [SerializeField]
    private ObjectiveStatus _status;
 
    private GameObject Target;
    [SerializeField]
    private GameEventArgs QuestStarted;
    [SerializeField]
    private GameEventArgs QuestEnded;
    [SerializeField]
    private GameEventArgs QuestChange;
    [SerializeField]
    private UnityEngine.Events.UnityEvent actionsOnComplete;

    /// <summary>
    /// move this objective forward in its current state
    /// None->Inactive, Inactive-> Active, Active-> Active, Active -> Complete
    /// Invoke the questchange event everytime we changestate
    /// invoke the questend when going from active -> complete
    /// </summary>
    public void ProgressQuest()
    {
        switch (_status)
        {
            case ObjectiveStatus.None://0
                ChangeState(ObjectiveStatus.Inactive);
                break;
            case ObjectiveStatus.Inactive://1
                ChangeState(ObjectiveStatus.Active);
                QuestStarted.Raise(this);
                break;
            case ObjectiveStatus.Active://2
                CurrentAmount++;
                if (CurrentAmount >= RequiredAmount)
                    ChangeState(ObjectiveStatus.Complete);
                else
                    ChangeState(ObjectiveStatus.Active);
                break;
            case ObjectiveStatus.Complete://3
                QuestEnded.Raise(this);
                actionsOnComplete.Invoke();
                break;            
        }        
    }

    private void ChangeState(ObjectiveStatus state)
    {
        _status = state;
        QuestChange.Raise(this);
    }    
}

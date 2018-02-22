using System;
using System.Linq;
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

//public enum ActionOnReach
//{
//    MarkComplete = 0,
//    AddModififer = 1,
//    PlayAudio = 2,
//    PlayAnimation = 3,
//}
[CreateAssetMenu]
public class Objective : ScriptableObject
{

    [SerializeField]
    private string Title;
    [Multiline]

    [SerializeField]
    private string _description;
    public string Description
    {
        get { return _description; }
    }

    [SerializeField]
    public ObjectiveType MissionType;
    [SerializeField]
    private Item _requiredItem;

    [SerializeField]
    private int _currentAmount;

    public int CurrentAmount
    {
        get { return _currentAmount; }
    }

    [SerializeField]
    private int _requiredAmount = 5;

    public int RequiredAmount
    {
        get { return _requiredAmount; }
    }

    public ObjectiveStatus Status
    {
        get
        {
            return _status;
        }

        set
        {
            _status = value;
        }
    }

    [SerializeField]
    private ObjectiveStatus _status;
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
    /// invoke the questend when going from active -> complete    /// 
    /// </summary>
    /// <param>the item we are using to progress this quest
    ///     <name>item</name>
    /// </param>
    public void ProgressQuest(params object[] args)
    {
        if (args[0] == null)
            return;
        Debug.Log("Quest Progress: " + Title + " " + args[0]);
        var valids = new object[] { _requiredItem, "initialize", "start"};
        
        if (!valids.Contains(args[0]))
            return;

        switch (Status)
        {
            case ObjectiveStatus.None:
                ChangeState(ObjectiveStatus.Inactive);
                break;
            case ObjectiveStatus.Inactive:
                ChangeState(ObjectiveStatus.Active);                
                break;
            case ObjectiveStatus.Active:
                _currentAmount++;
                ChangeState(_currentAmount >= _requiredAmount ? ObjectiveStatus.Complete : ObjectiveStatus.Active);
                break;
            case ObjectiveStatus.Complete:                  
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    /// <summary>
    /// successful valid state change so now do stuff
    /// </summary>
    /// <param name="state"></param>
    private void ChangeState(ObjectiveStatus state)
    {
        Status = state;
        QuestChange.Raise(this);
        switch (Status)
        {
            case ObjectiveStatus.None:               
                break;
            case ObjectiveStatus.Inactive:              
                QuestStarted.Raise(this);
                break;
            case ObjectiveStatus.Active:        
                break;
            case ObjectiveStatus.Complete:
                QuestEnded.Raise(this);
                //actionsOnComplete.Invoke();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        } 
    }
}

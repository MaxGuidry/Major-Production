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
    private int _currentAmount = 0;

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
    /// <param name="item">the item we are using to progress this quest</param>
    public void ProgressQuest(params object[] args)
    {
        if (args[0] == null)
            return;
        Debug.Log("progress quest" + args[0].ToString());
        var valids = new object[] { _requiredItem, "initialize", "start"};
        
        if (!valids.Contains(args[0]))
            return;

        switch (_status)
        {
            case ObjectiveStatus.None://0
                ChangeState(ObjectiveStatus.Inactive);
                break;
            case ObjectiveStatus.Inactive://1
                ChangeState(ObjectiveStatus.Active);                
                break;
            case ObjectiveStatus.Active://2
                _currentAmount++;
                if (_currentAmount >= _requiredAmount)
                    ChangeState(ObjectiveStatus.Complete);
                else
                    ChangeState(ObjectiveStatus.Active);
                break;
            case ObjectiveStatus.Complete://3                   
                break;
        }
    }

    /// <summary>
    /// successful valid state change so now do stuff
    /// </summary>
    /// <param name="state"></param>
    private void ChangeState(ObjectiveStatus state)
    {
        _status = state;
        QuestChange.Raise(this);
        switch (_status)
        {
            case ObjectiveStatus.None://0                
                break;
            case ObjectiveStatus.Inactive://1                
                QuestStarted.Raise(this);
                break;
            case ObjectiveStatus.Active://2        
                break;
            case ObjectiveStatus.Complete://3
                QuestEnded.Raise(this);
                //actionsOnComplete.Invoke();
                break;
        } 
    }
}

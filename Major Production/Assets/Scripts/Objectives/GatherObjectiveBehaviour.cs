using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ScriptableObjects;
public class GatherObjectiveBehaviour : MonoBehaviour
{
    public Objective CurrentObjective;
    public List<Objective> PlayerObjectives;
    private void Start()
    {
        foreach (var i in GameObject.FindGameObjectsWithTag("Gather"))
        {
            PlayerObjectives.Add(i.GetComponent<ObjectiveCollider>().Obj);
        }
        PlayerObjectives.Reverse();
        if (PlayerObjectives != null)
        {
            Debug.Log("Found All Gather Objectives!");
            foreach (var Objective in PlayerObjectives)
            {
                if (Objective != null)
                    Objective.Status = ObjectiveStatus.Inactive;
            }
        }
        PlayerObjectives[0].Status = ObjectiveStatus.Active;
        if (PlayerObjectives[0].Status == ObjectiveStatus.Active)
        {
            CurrentObjective = PlayerObjectives[0];
            CurrentObjective.QuestStarted.Raise(this, CurrentObjective.RequiredItem);
        }
    }

    public void ProgressQuest(UnityEngine.Object[] args)
    {
        var sender = args[0] as Item;
        if (sender == CurrentObjective.RequiredItem)
        {
            CurrentObjective.CurrentAmount++;
        }
        if (CurrentObjective.CurrentAmount >= CurrentObjective.RequiredAmount)
        {
            CurrentObjective.OnReach(CurrentObjective);
            CurrentObjective.QuestEnded.Raise(this, CurrentObjective.RequiredItem);
        }
        CurrentObjective.QuestChange.Raise(this, CurrentObjective.RequiredItem);
    }
}

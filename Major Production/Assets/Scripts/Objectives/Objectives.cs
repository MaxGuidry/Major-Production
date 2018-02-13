using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ScriptableObjects;
public class Objectives : MonoBehaviour
{
    public Objective CurrentObjective;
    public List<Objective> PlayerObjectives;
    public Text CurrentObjectiveText;

    private void Start()
    {
        var objParent = transform.gameObject;
        if (objParent != null)
        {
            foreach (var i in GameObject.FindGameObjectsWithTag("Objective"))
            {
                PlayerObjectives.Add(i.GetComponent<ObjectiveCollider>().Obj);
            }
            PlayerObjectives.Reverse();
            if (PlayerObjectives != null)
            {
                Debug.Log("Found All Objectives!");
                foreach (var Objective in PlayerObjectives)
                {
                    if (Objective != null)
                    {
                        Objective.ParentScript = this;
                        Objective.Status = Objective.ObjectiveStatus.None;
                    }
                }
            }
            PlayerObjectives[0].Status = Objective.ObjectiveStatus.Active;
            if (PlayerObjectives[0].Status == Objective.ObjectiveStatus.Active)
            {
                CurrentObjective = PlayerObjectives[0];
                CurrentObjective.QuestStarted.Raise(this, CurrentObjective.RequiredItem);
            }

        }
        else
            Debug.Log("Unable To Find Objectives!");
    }
    public void OnGui()
    {
        CurrentObjectiveText.text = CurrentObjective.Description + " " + CurrentObjective.CurrentAmount + " / " + CurrentObjective.RequiredAmount;
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
            if (PlayerObjectives[0].Status == Objective.ObjectiveStatus.Complete)
            {
                PlayerObjectives.Remove(PlayerObjectives[0]);
                PlayerObjectives[0].Status = Objective.ObjectiveStatus.Active;
            }
            CurrentObjective.QuestEnded.Raise(this, CurrentObjective.RequiredItem);
        }
        if (PlayerObjectives[0].Status == Objective.ObjectiveStatus.Active)
        {
            CurrentObjective = PlayerObjectives[0];
        }
        CurrentObjective.QuestChange.Raise(this, CurrentObjective.RequiredItem);
        PlayerObjectives[0].Status = Objective.ObjectiveStatus.Active;
    }
}

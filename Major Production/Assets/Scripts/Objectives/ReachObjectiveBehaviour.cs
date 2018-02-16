using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ScriptableObjects;
public class ReachObjectiveBehaviour : MonoBehaviour
{
    public Objective CurrentObjective;
    public List<Objective> PlayerObjectives;

    private void Start()
    {
        foreach (var i in GameObject.FindGameObjectsWithTag("Reach"))
        {
            PlayerObjectives.Add(i.GetComponent<ObjectiveCollider>().Obj);
        }
        if (PlayerObjectives != null)
        {
            Debug.Log("Found All Reach Objectives!");
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
            CurrentObjective.QuestStarted.Raise(this, CurrentObjective.Target);
        }
    }

    public void ProgressQuest()
    {
        CurrentObjective.OnReach(CurrentObjective);
        CurrentObjective.QuestEnded.Raise(this, CurrentObjective.Target);
        CurrentObjective.QuestChange.Raise(this, CurrentObjective.Target);
    }
}

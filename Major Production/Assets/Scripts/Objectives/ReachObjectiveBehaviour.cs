using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ScriptableObjects;
public class ReachObjectiveBehaviour : MonoBehaviour
{
    public Objective CurrentObjective;
    public List<Objective> PlayerObjectives;
    public Text CurrentObjectiveText;
    private Transform Player;
    private bool check;
    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Start()
    {
        var objParent = transform.gameObject;
        if (objParent != null)
        {
            foreach (var i in GameObject.FindGameObjectsWithTag("Reach"))
            {
                PlayerObjectives.Add(i.GetComponent<ObjectiveCollider>().Obj);
            }
            PlayerObjectives.Reverse();
            if (PlayerObjectives != null)
            {
                Debug.Log("Found All Reach Objectives!");
                foreach (var Objective in PlayerObjectives)
                {
                    if (Objective != null)
                    {
                        //Objective.ReachParentScript = this;
                        Objective.Status = Objective.ObjectiveStatus.Inactive;
                    }
                }
            }
            PlayerObjectives[0].Status = Objective.ObjectiveStatus.Active;
            if (PlayerObjectives[0].Status == Objective.ObjectiveStatus.Active)
            {
                CurrentObjective = PlayerObjectives[0];
                CurrentObjective.QuestStarted.Raise(this, CurrentObjective.Target);
            }

        }
        else
            Debug.Log("Unable To Find Objectives!");
    }
    public void OnGui()
    {
        check = false;
        if (CurrentObjective.MissionType == Objective.ObjectiveType.Reach)
        {
            CurrentObjectiveText.text = CurrentObjective.Description + " " + CurrentObjective.Target.transform.position.ToString();
            if (CurrentObjective.Target == null)
            {
                Debug.LogError("Please Set A Target Destination For " + CurrentObjective.name);
            }
        }
        else
        {
            Debug.LogError("Please Set Quest To Reach");
        }
    }
    public void ProgressQuest()
    {
        CurrentObjective.OnReach(CurrentObjective);
        Debug.Log("Reached");
        if (PlayerObjectives[0].Status == Objective.ObjectiveStatus.Complete)
        {
            PlayerObjectives.Remove(PlayerObjectives[0]);
            PlayerObjectives[0].Status = Objective.ObjectiveStatus.Active;
        }
        if (PlayerObjectives.Count >= 1)
        {
            CurrentObjective.QuestEnded.Raise(this, CurrentObjective.Target);
            if (PlayerObjectives[0].Status == Objective.ObjectiveStatus.Active)
            {
                CurrentObjective = PlayerObjectives[0];
            }
            CurrentObjective.QuestChange.Raise(this, CurrentObjective.Target);
            PlayerObjectives[0].Status = Objective.ObjectiveStatus.Active;
        }
    }
}

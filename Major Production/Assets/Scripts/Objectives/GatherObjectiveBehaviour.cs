using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ScriptableObjects;
public class GatherObjectiveBehaviour : MonoBehaviour
{
    public Objective CurrentObjective;
    public List<Objective> PlayerObjectives;
    public Text CurrentObjectiveText;
    private Transform Player;
    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Start()
    {
        var objParent = transform.gameObject;
        if (objParent != null)
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
                    {
                        // Objective.GatherParentScript = this;
                        Objective.Status = Objective.ObjectiveStatus.Inactive;
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
        if (PlayerObjectives.Count == 0)
        {
            CurrentObjectiveText.enabled = false;
            return;
        }
        if (CurrentObjective != null)
        {
            if (CurrentObjective.MissionType == Objective.ObjectiveType.Gather)
            {
                CurrentObjectiveText.text = CurrentObjective.Description + " " + CurrentObjective.CurrentAmount + " / " + CurrentObjective.RequiredAmount;
                if (CurrentObjective.RequiredAmount == 0)
                    Debug.LogError("Please Set A Required Item For " + CurrentObjective.name);
            }
            else
                Debug.LogError("Please Set Quest To Gather");
        }
    }
    public void ProgressQuest(UnityEngine.Object[] args)
    {
        var sender = args[0] as Item;
        if (PlayerObjectives.Count == 0)
        {
            gameObject.SetActive(false);
            return;
        }
        if (CurrentObjective != null)
        {
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
                    if (PlayerObjectives.Count != 0)
                        PlayerObjectives[0].Status = Objective.ObjectiveStatus.Active;
                }
                CurrentObjective.QuestEnded.Raise(this, CurrentObjective.RequiredItem);
            }
            if (PlayerObjectives.Count != 0)
            {
                if (PlayerObjectives[0].Status == Objective.ObjectiveStatus.Active)
                {
                    CurrentObjective = PlayerObjectives[0];
                }
            }
            CurrentObjective.QuestChange.Raise(this, CurrentObjective.RequiredItem);
        }
    }
}

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
    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Start()
    {
        var objParent = transform.gameObject;//when is this ever null
        if (objParent != null)
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
                    {
                        //Objective.ReachParentScript = this;
                        Objective.Status = ObjectiveStatus.Inactive;
                    }
                }
            }
            PlayerObjectives[0].Status = ObjectiveStatus.Active;
            if (PlayerObjectives[0].Status == ObjectiveStatus.Active)
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
        if (CurrentObjective != null)
        {
            if (CurrentObjective.MissionType == ObjectiveType.Reach)
            {
                CurrentObjectiveText.text = CurrentObjective.Description + " " + CurrentObjective.Target.transform.position.ToString();
                if (CurrentObjective.Target == null)
                    Debug.LogError("Please Set A Target Destination For " + CurrentObjective.name);
                if (PlayerObjectives.Count == 0)
                {
                    CurrentObjectiveText.enabled = false;
                    gameObject.SetActive(false);
                    return;
                }
            }
            else
                Debug.LogError("Please Set Quest To Reach");
        }
    }
    public void ProgressQuest()
    {

        CurrentObjective.OnReach(CurrentObjective);
        if (PlayerObjectives[0].Status == ObjectiveStatus.Complete)
        {
            PlayerObjectives.Remove(PlayerObjectives[0]);
            if (PlayerObjectives.Count != 0)
                PlayerObjectives[0].Status = ObjectiveStatus.Active;
        }
        CurrentObjective.QuestEnded.Raise(this, CurrentObjective.Target);
        if (PlayerObjectives.Count != 0)
        {
            if (PlayerObjectives[0].Status == ObjectiveStatus.Active)
            {
                CurrentObjective = PlayerObjectives[0];
            }  
        }
        CurrentObjective.QuestChange.Raise(this, CurrentObjective.Target);
    }
}

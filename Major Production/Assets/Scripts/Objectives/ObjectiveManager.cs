using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveManager : MonoBehaviour {
    public void TestNext(UnityEngine.Object[] args)
    {
        var PlayerObjectives = (Objective[])args[0];
    }
    public void SetNext(List<Objective> PlayerObjectives, Objective CurrentObjective)
    {
        if (PlayerObjectives[0].Status == ObjectiveStatus.Complete)
        {
            PlayerObjectives.Remove(PlayerObjectives[0]);
            if (PlayerObjectives.Count != 0)
                PlayerObjectives[0].Status = ObjectiveStatus.Active;
        }
        if (PlayerObjectives.Count != 0)
        {
            if (PlayerObjectives[0].Status == ObjectiveStatus.Active)
            {
                CurrentObjective = PlayerObjectives[0];
            }
        }
    }

    public void CheckIfDestroy(List<Objective> PlayerObjectives, Text CurrentObjectiveText, GameObject Objective)
    {
        if (PlayerObjectives.Count <= 0)
        {
            CurrentObjectiveText.enabled = false;
            Objective.SetActive(false);
        }
    }

    public void OnGui(Text CurrentObjectiveText, Objective CurrentObjective)
    {
        CurrentObjectiveText.text = CurrentObjective.Description + " " + CurrentObjective.CurrentAmount + " / " + CurrentObjective.RequiredAmount;

        if (CurrentObjective.RequiredAmount == 0)
            Debug.LogError("Please Set A Required Item For " + CurrentObjective.name);
    }
}

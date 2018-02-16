using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ObjectiveManager : MonoBehaviour {
    public void SetNext(UnityEngine.Object[] args)
    {
        var sender = args[0];
        if (sender != null)
        {
            var Objectives = (object)args[1] as Objective[];
            var PlayerObjectives = Objectives.ToList();
            var CurrentObjective = PlayerObjectives[0];

            if (PlayerObjectives[0].Status == ObjectiveStatus.Complete)
            {
                PlayerObjectives.Remove(PlayerObjectives[0]);
                if (PlayerObjectives.Count != 0)
                    PlayerObjectives[0].Status = ObjectiveStatus.Active;
            }

            if (PlayerObjectives.Count != 0)
                if (PlayerObjectives[0].Status == ObjectiveStatus.Active)
                    CurrentObjective = PlayerObjectives[0];
        }
    }

    //public void CheckIfDestroy(List<Objective> PlayerObjectives, Text CurrentObjectiveText, GameObject Objective)
    //{
    //    if (PlayerObjectives.Count <= 0)
    //    {
    //        CurrentObjectiveText.enabled = false;
    //        Objective.SetActive(false);
    //    }
    //}

    //public void OnGui(Text CurrentObjectiveText, Objective CurrentObjective)
    //{
    //    CurrentObjectiveText.text = CurrentObjective.Description + " " + CurrentObjective.CurrentAmount + " / " + CurrentObjective.RequiredAmount;

    //    if (CurrentObjective.RequiredAmount == 0)
    //        Debug.LogError("Please Set A Required Item For " + CurrentObjective.name);
    //}
}

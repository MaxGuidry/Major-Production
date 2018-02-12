using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        }
        else
            Debug.Log("Unable To Find Objectives!");
    }
    private void Update()
    {
        PlayerObjectives[0].Status = Objective.ObjectiveStatus.Active;
        if (PlayerObjectives[0].Status == Objective.ObjectiveStatus.Active)
            CurrentObjective = PlayerObjectives[0];
        if (PlayerObjectives[0].Status == Objective.ObjectiveStatus.Complete)
            PlayerObjectives.Remove(PlayerObjectives[0]);
    }
    private void OnGUI()
    {
        CurrentObjectiveText.text = CurrentObjective.Description;
    }
}

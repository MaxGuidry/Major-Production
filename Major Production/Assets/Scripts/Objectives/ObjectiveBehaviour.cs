using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[RequireComponent(typeof(AudioSource))]
public class ObjectiveBehaviour : MonoBehaviour
{
    public Objective CurrentObjective;
    public List<Objective> PlayerObjectives;
    private void Start()
    {
        PlayerObjectives.ForEach(objective => objective.ProgressQuest());
        //set the current objective
        CurrentObjective = PlayerObjectives[0];
        //activate the currentobjective
        CurrentObjective.ProgressQuest();
    }
}

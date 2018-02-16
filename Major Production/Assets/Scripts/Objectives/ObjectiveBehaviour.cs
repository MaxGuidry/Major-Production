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
        //this is bad for future donray
        //null should be used to check validity of the token
        PlayerObjectives.ForEach(objective => objective.ProgressQuest("initialize"));
        //set the current objective
        CurrentObjective = PlayerObjectives[0];
        //activate the currentobjective
        CurrentObjective.ProgressQuest("start");
    }
}

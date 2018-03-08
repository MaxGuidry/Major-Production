using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

public class ObjectiveBehaviour : MonoBehaviour
{
    public Objective CurrentObjective;
    public List<Objective> PlayerObjectives;
    public GameEvent AllQuestComplete;

    private void Start()
    {
        //null should be used to check validity of the token
        if (PlayerObjectives == null) return;
        PlayerObjectives.ForEach(objective => objective.ProgressQuest("initialize"));
        //set the current objective
        CurrentObjective = PlayerObjectives[0];
        //activate the currentobjective
        CurrentObjective.ProgressQuest("start");
    }

    /// <summary>
    ///     Checks if the objectives are the same then progresses
    /// </summary>
    /// <param name="args"></param>
    public void ProgressChain(Object[] args)
    {
        if (args[0] != CurrentObjective)
            return;
        ProgressChain();
    }

    /// <summary>
    ///     Moves to the next quest in the list
    /// </summary>
    public virtual void ProgressChain()
    {
        if (CurrentObjective == null)
            return;

        //Set Next Objective
        PlayerObjectives.Remove(CurrentObjective);
        if (PlayerObjectives.Count != 0)
        {
            CurrentObjective = PlayerObjectives[0];
            CurrentObjective.Status = ObjectiveStatus.Active;
        }
    }

    /// <summary>
    ///     Progress the objective status
    /// </summary>
    /// <param name="args"></param>
    public void ProgressObjective(Object[] args)
    {
        var sender = args[0];
        CurrentObjective.ProgressQuest(sender as Item);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class ReachObjectiveBehaviour : ObjectiveBehaviour
{
    void onQuestStart(Object[] args)
    {
        if(args[0] == CurrentObjective)
        {

        }
    }
    void onQuestCompleted(Object[] args)
    {
        if (args[0] == CurrentObjective)
        {
            PlayerObjectives.Remove(CurrentObjective);
            CurrentObjective = PlayerObjectives[0];
        }
    }
}

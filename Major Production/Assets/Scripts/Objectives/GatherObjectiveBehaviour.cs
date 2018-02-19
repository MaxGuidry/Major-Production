
public class GatherObjectiveBehaviour : ObjectiveBehaviour
{
    public UnityEngine.UI.Text CurrentObjectiveText;

    public void UI_RefreshGather()
    {
        if (CurrentObjective == null)
            return;
        CurrentObjectiveText.text = CurrentObjective.Description + " " + CurrentObjective.CurrentAmount + " / " + CurrentObjective.RequiredAmount;
    }

    public void ProgressGather(UnityEngine.Object[] args)
    {
        var sender = args[0]; //this is an item
        CurrentObjective.ProgressQuest(sender as ScriptableObjects.Item);
    }

    public void ProgressGatherChain()
    {
        if (PlayerObjectives.Count <= 0)
            Destroy(gameObject);
        if (CurrentObjective != null)
        {
            if (PlayerObjectives.Contains(CurrentObjective))
            {
                PlayerObjectives.Remove(CurrentObjective);
                if (PlayerObjectives.Count != 0)
                    CurrentObjective = PlayerObjectives[0];
                else
                {
                    Destroy(CurrentObjectiveText);
                    Destroy(gameObject);
                }
            }
            else
                Destroy(gameObject);
        }
        //PlayerObjectives.Remove(CurrentObjective);
        //CurrentObjective = PlayerObjectives[0];
    }
}

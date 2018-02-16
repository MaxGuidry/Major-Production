 
public class GatherObjectiveBehaviour : ObjectiveBehaviour
{
    public UnityEngine.UI.Text CurrentObjectiveText;

    public void UI_Refresh()
    {
        if (CurrentObjective == null)
            return;
        CurrentObjectiveText.text = CurrentObjective.Description + " " + CurrentObjective.CurrentAmount + " / " + CurrentObjective.RequiredAmount;
    }
    
    public void ProgressQuest(UnityEngine.Object[] args)
    {
        var sender = args[0]; //this is an item
        CurrentObjective.ProgressQuest(sender as ScriptableObjects.Item);
    }

    public void ProgressQuestChain()
    {
        PlayerObjectives.Remove(CurrentObjective);
        if (PlayerObjectives.Count <= 0)
            Destroy(gameObject);
        CurrentObjective = PlayerObjectives[0];
    }
}

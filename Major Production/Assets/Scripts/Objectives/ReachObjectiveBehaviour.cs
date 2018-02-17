
public class ReachObjectiveBehaviour : ObjectiveBehaviour
{
    public UnityEngine.UI.Text CurrentObjectiveText;

    public void UI_Refresh()
    {
        if (CurrentObjective == null)
            return;
        CurrentObjectiveText.text = CurrentObjective.Description;
    }

    public void ProgressQuest(UnityEngine.Object[] args)
    {
        var sender = args[0];
        CurrentObjective.ProgressQuest(sender as ScriptableObjects.Item);
    }

    public void ProgressQuestChain()
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
    }
}

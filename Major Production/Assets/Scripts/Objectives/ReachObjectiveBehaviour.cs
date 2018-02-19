
public class ReachObjectiveBehaviour : ObjectiveBehaviour
{
    public UnityEngine.UI.Text CurrentObjectiveText;

    public void UI_RefreshReach()
    {
        if (CurrentObjective == null)
            return;
        CurrentObjectiveText.text = CurrentObjective.Description;
    }

    public void ProgresstReach(UnityEngine.Object[] args)
    {
        var sender = args[0];
        CurrentObjective.ProgressQuest(sender as ScriptableObjects.Item);
    }

    public void ProgressReachChain()
    {
        UnityEngine.Debug.Log("ProgressReachChain");
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

public class ReachObjectiveBehaviour : ObjectiveBehaviour
{
    public UnityEngine.UI.Text CurrentObjectiveText;

    public void UI_RefreshReach()
    {
        if (CurrentObjective == null)
            return;
        if (PlayerObjectives.Count <= 0)
            Destroy(CurrentObjectiveText);
        CurrentObjectiveText.text = CurrentObjective.Description;
    }


    public override void ProgressChain()
    {
        base.ProgressChain();
    }
}

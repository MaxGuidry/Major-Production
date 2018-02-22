public class GatherObjectiveBehaviour : ObjectiveBehaviour
{
    public UnityEngine.UI.Text CurrentObjectiveText;

    public void UI_RefreshGather()
    {
        if (CurrentObjective == null)
            return;
        if (PlayerObjectives.Count <= 0)
            Destroy(CurrentObjectiveText);
        CurrentObjectiveText.text = CurrentObjective.Description + " " + CurrentObjective.CurrentAmount + " / " + CurrentObjective.RequiredAmount;
    }


    public override void ProgressChain()
    {
        base.ProgressChain();
    }
}

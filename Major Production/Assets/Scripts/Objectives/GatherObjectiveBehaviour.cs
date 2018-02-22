using UnityEngine.UI;

public class GatherObjectiveBehaviour : ObjectiveBehaviour
{
    public Text CurrentObjectiveText;
    /// <summary>
    /// 
    /// </summary>
    public void UI_RefreshGather()
    {
        if (CurrentObjective == null)
            return;
        if (PlayerObjectives.Count <= 0)
            Destroy(CurrentObjectiveText);
        CurrentObjectiveText.text = CurrentObjective.Description + " " +
                                    CurrentObjective.CurrentAmount + " / " +
                                    CurrentObjective.RequiredAmount;
    }
    /// <summary>
    /// 
    /// </summary>
    public override void ProgressChain()
    {
        base.ProgressChain();
    }
}
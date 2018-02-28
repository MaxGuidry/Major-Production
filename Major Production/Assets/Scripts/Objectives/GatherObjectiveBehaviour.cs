using UnityEngine.UI;

public class GatherObjectiveBehaviour : ObjectiveBehaviour
{
    public Text CurrentObjectiveText;

    /// <summary>
    ///     Checks if you have a current objective and if the list is not empty
    ///     Sets the text
    /// </summary>
    public void UI_RefreshGather()
    {
        if (CurrentObjective == null) return;
        if (PlayerObjectives.Count <= 0) Destroy(CurrentObjectiveText);
        CurrentObjectiveText.text = CurrentObjective.Description + " " +
                                    CurrentObjective.CurrentAmount + " / " +
                                    CurrentObjective.RequiredAmount;
    }

    /// <summary>
    ///     Not important for now
    /// </summary>
    public override void ProgressChain()
    {
        base.ProgressChain();
    }
}
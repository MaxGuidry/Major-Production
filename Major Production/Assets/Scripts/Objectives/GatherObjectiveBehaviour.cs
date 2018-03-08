using UnityEngine;
using UnityEngine.UI;

public class GatherObjectiveBehaviour : ObjectiveBehaviour
{
    public Text CurrentObjectiveText;
    public AutoTyper typer;
    /// <summary>
    ///     Checks if you have a current objective and if the list is not empty
    ///     Sets the text
    /// </summary>
    public void UI_RefreshGather()
    {
        if (CurrentObjective == null) return;
        if (PlayerObjectives.Count <= 0)
        {
            AllQuestComplete.Raise();
            Destroy(CurrentObjectiveText);
        }
        CurrentObjectiveText.text = CurrentObjective.Description + " " +
                                    CurrentObjective.CurrentAmount + " / " +
                                    CurrentObjective.RequiredAmount;
    }

    public void AutoType()
    {
        if (typer == null) return;
        typer.ChooseFile = "AllQuestComplete";
        typer.TypeSpeed = 0.1f;
        typer.path = "Assets/Resources/Dialogue/" + typer.ChooseFile + ".txt";
        StartCoroutine(typer.AutoType());
    }

    /// <summary>
    ///     Not important for now
    /// </summary>
    public override void ProgressChain()
    {
        base.ProgressChain();
    }
}
 
using UnityEngine.UI;
 
public class GatherObjectiveBehaviour : ObjectiveBehaviour
{
    public Text CurrentObjectiveText;

    public void UI_Refresh()
    {
        CurrentObjectiveText.text = CurrentObjective.description + " " + CurrentObjective.currentAmount + " / " + CurrentObjective.requiredAmount;
    }
}

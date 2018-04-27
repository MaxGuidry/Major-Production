using System.Collections;
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
            var capCollider = this.gameObject.transform.parent.GetChild(0).GetComponent<CapsuleCollider>();
            AllQuestComplete.Raise();
            CurrentObjectiveText.text = "Kill All Players!";
            //this.gameObject.transform.parent.GetChild(0).transform.localScale *= 2;
            capCollider.radius /= 2;
            capCollider.height /= 2;
            capCollider.center = new Vector3(capCollider.center.x, capCollider.center.y / 2, capCollider.center.z);
            gameObject.transform.parent.GetChild(0).GetComponent<TweenScaleBehaviour>().StartCoroutine(gameObject
                .transform.parent.GetChild(0).GetComponent<TweenScaleBehaviour>().TweenItForwardOnly());
            //this.gameObject.transform.parent.GetChild(0).transform.localScale = new Vector3(10, 10, 10);
            //Destroy(CurrentObjectiveText);
        }
        else
        {
            CurrentObjectiveText.text = CurrentObjective.Description + " " +
                                        CurrentObjective.CurrentAmount + " / " +
                                        CurrentObjective.RequiredAmount;
        }

    }

    /// <summary>
    ///     If quest is complete set the text and call AutoType function
    /// </summary>
    public void AutoType()
    {
        if (typer == null) return;
        typer.ChooseFile = "AllQuestComplete";
        typer.TypeSpeed = 0.1f;
        typer.path = "Assets/Resources/Dialogue/" + typer.ChooseFile + ".txt";
        //CurrentObjectiveText.text = "Kill All Players!";
        //StartCoroutine(typer.AutoType());
        //StartCoroutine(Restart());
    }


    private IEnumerator Restart()
    {
        yield return new WaitForSeconds(3);
        GameManagerBehaviour.RestartGame();
    }
    /// <summary>
    ///     Not important for now
    /// </summary>
    public override void ProgressChain()
    {
        base.ProgressChain();
    }
}
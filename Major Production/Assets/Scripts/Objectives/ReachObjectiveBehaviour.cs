using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class ReachObjectiveBehaviour : ObjectiveBehaviour
{
    private AudioSource _Audio;
    public AudioClip Clip;
    public Text CurrentObjectiveText;

    public AutoTyper typer;

    /// <summary>
    ///     Checks if you have a current objective and if the list is not empty
    ///     Sets the text
    /// </summary>
    public void UI_RefreshReach()
    {
        if (CurrentObjective == null) return;
        if (PlayerObjectives.Count <= 0) Destroy(CurrentObjectiveText);
        CurrentObjectiveText.text = CurrentObjective.Description;
    }
    ///// <summary>
    /////     Checks if the objectives are the same then progresses
    ///// </summary>
    ///// <param name="args"></param>
    //public void PlayAudio(Object[] args)
    //{
    //    if (args[0] != CurrentObjective)
    //        return;
    //    PlayAudio();
    //}
    ///// <summary>
    /////     Plays Audio from the AudioSource
    ///// </summary>
    //private void PlayAudio()
    //{
    //    _Audio = GetComponent<AudioSource>();
    //    if (_Audio.isPlaying) return;
    //    _Audio.clip = Clip;
    //    _Audio.Play();
    //}

    /// <summary>
    ///     Call the AutoType Function
    /// </summary>
    public void AutoType()
    {
        if (typer == null) return;
        typer.ChooseFile = "CheckPointReached";
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
        if (typer == null) return;
        typer.ChooseFile = "ReachProgress";
        typer.TypeSpeed = 0.1f;
        typer.path = "Assets/Resources/Dialogue/" + typer.ChooseFile + ".txt";
        StartCoroutine(typer.AutoType());
    }
}
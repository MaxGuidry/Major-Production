using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class ReachObjectiveBehaviour : ObjectiveBehaviour
{
    private AudioSource _Audio;

    public AudioClip Clip;
    public Text CurrentObjectiveText;
    /// <summary>
    /// 
    /// </summary>
    public void UI_RefreshReach()
    {
        if (CurrentObjective == null)
            return;
        if (PlayerObjectives.Count <= 0)
            Destroy(CurrentObjectiveText);
        CurrentObjectiveText.text = CurrentObjective.Description;
    }
    /// <summary>
    /// 
    /// </summary>
    public void PlayAudio()
    {
        _Audio = GetComponent<AudioSource>();
        if (_Audio.isPlaying) return;
        _Audio.clip = Clip;
        _Audio.Play();
    }
    /// <summary>
    /// 
    /// </summary>
    public override void ProgressChain()
    {
        base.ProgressChain();
    }
}
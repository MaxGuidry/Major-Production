using System.Collections;
using UnityEngine;
public class TweenScaleBehaviour : MonoBehaviour
{
    public AnimationCurve ac;
    public float duration = 1;
    public GameObject tweened;
    private bool isRunning = false;
    public void TweenScale()
    {
        if(!isRunning)
            StartCoroutine(TweenItForward());
    }

    public IEnumerator TweenItForward()
    {
        isRunning = true;
        var timer = 0.0f;
        var startScale = tweened.transform.localScale;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            tweened.transform.localScale = startScale + Vector3.one * ac.Evaluate(timer / duration);
            yield return null;
        }

        StartCoroutine(TweenItBackward());

        yield return null;
    }

    public IEnumerator TweenItBackward()
    {
        var timer = 0.0f;
        var startScale = tweened.transform.localScale;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            tweened.transform.localScale = startScale + Vector3.one * ac.Evaluate(timer / duration) * -1.0f;
            yield return null;
        }
        isRunning = false;
        yield return null;
    }
}
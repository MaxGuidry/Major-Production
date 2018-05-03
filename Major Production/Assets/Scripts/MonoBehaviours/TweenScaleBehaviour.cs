using System.Collections;
using UnityEngine;
public class TweenScaleBehaviour : MonoBehaviour
{
    public AnimationCurve ac;
    public float duration = 1;
    public GameObject tweened;
    public bool isRunning = false;
    public void TweenScale()
    {
        if (!gameObject.activeInHierarchy) return;
        if(!isRunning)
            StartCoroutine(TweenItForward());
    }

    public IEnumerator TweenItForward()
    {
        if (!tweened.activeInHierarchy) yield return null;
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
        if (!tweened.activeInHierarchy) yield return null;
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

    public IEnumerator TweenItForwardOnly()
    {
        isRunning = true;
        var timer = 0.0f;
        var startScale = tweened.transform.localScale;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            tweened.transform.localScale = startScale + Vector3.one * ac.Evaluate(timer / duration);
            yield return null;
            tweened.transform.localScale = new Vector3(10, 10, 10);
        }

       // StartCoroutine(TweenItBackward());

        yield return null;
    }

}
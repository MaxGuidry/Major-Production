using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudioManager : MonoBehaviour
{
    public List<AudioSource> Sources;
    private bool wonPlayed, lostPlayed, onePlayed, twoPlayed, threePlayed, fourPlayed, fivePlayed = false;

    private CountDown countDown;

    private AudioSource music;
    // Use this for initialization
    void Start()
    {
        countDown = GetComponent<CountDown>();
        foreach (var audioSource in GetComponents<AudioSource>())
        {
            Sources.Add(audioSource);
            if (audioSource.clip.name == "GameActiveShorter")
            {
                music = audioSource;
                StartCoroutine(FadeMusicIn());
            }
        }
    }

    IEnumerator FadeMusicIn()
    {
        music.volume = 0;

        while (music.volume < 1)
        {
            music.volume += .005f;
            yield return null;
        }

    }

    IEnumerator FadeOUt()
    {
        music.volume = 1;

        while (music.volume > 0)
        {
            music.volume -= .005f;
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (countDown.activePlayers.Count == 1)
        {
            foreach (var audioSource in Sources)
            {
                if (audioSource.clip.name.Contains("Active"))
                {
                    var fadeOut = AudioFadeOut(audioSource, 900);
                    StartCoroutine(fadeOut);
                }
            }

            if (!wonPlayed)
            {
                PlayClip("Won");
                wonPlayed = true;
            }
        }

        if (countDown.Timer <= 0)
        {
            if (!lostPlayed)
            {
                PlayClip("Over");
                StartCoroutine(FadeOUt());
                lostPlayed = true;
            }
        }

        switch (countDown.TimerDisplay.text)
        {
            case "5":
                if (!fivePlayed)
                {
                    PlayCountdownClip("5");
                    fivePlayed = true;
                }
                break;
            case "4":
                if (!fourPlayed)
                {
                    PlayCountdownClip("4");
                    fourPlayed = true;
                }
                break;
            case "3":
                if (!threePlayed)
                {
                    PlayCountdownClip("3");
                    threePlayed = true;
                }
                break;
            case "2":
                if (!twoPlayed)
                {
                    PlayCountdownClip("2");
                    twoPlayed = true;
                }
                break;
            case "1":
                if (!onePlayed)
                {
                    PlayCountdownClip("1");
                    onePlayed = true;
                }
                break;

            default:
                break;
        }
    }

    void PlayCountdownClip(string clipName)
    {
        foreach (var audioSource in Sources)
        {
            if (audioSource.clip.name.Contains(clipName))
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }
        }
    }

    private void PlayClip(string clipName)
    {
        foreach (var audioSource in Sources)
        {
            if (audioSource.clip.name.Contains(clipName))
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }
        }
    }

    private IEnumerator AudioFadeOut(AudioSource audioSource, float fadeTime)
    {
        var startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }
    }
}

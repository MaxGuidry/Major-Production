using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Utility;

public class CountDown : MonoBehaviour
{
    [HideInInspector]public List<GameObject> activePlayers;
    public GameObject BackGround;
    public RawImage DisplayWinner;
    public RenderTexture TargeTexture;
    private bool GameActive;
    public GameObject GameOverUI;
    public List<GameObject> players;
    public List<Camera> SpecateCameras;
    public float Timer = 60f;
    private float deathTimer = 0f;
    public Text TimerDisplay;
    [HideInInspector]public float GameOverScreenTimer = 5f;
    [SerializeField]private float testTimer;
    public Image HourGlass;

    public Image CountdownImage;
    // Use this for initialization
    private void Start()
    {
        testTimer = 10;
        HourGlass.enabled = false;
        BackGround.SetActive(false);
        DisplayWinner.enabled = false;
        GameActive = true;
        activePlayers = new List<GameObject>();
        foreach (var SpecateCamera in SpecateCameras)
        {
            SpecateCamera.gameObject.SetActive(false);
            SpecateCamera.enabled = false;
        }

        foreach (var player in players)
        {
            activePlayers.Add(player);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (Timer >= 0f)
            TimerDisplay.text = Mathf.Round(Timer).ToString();
        if (players.Count == 0)
            return;
        if (Timer <= 0)
        {
            foreach (var player in players)
            {
                if (player == null) return;
                player.GetComponentInChildren<CharacterMovement>().enabled = false;
            }
            StartCoroutine(GameOverLost());
            StartHourGlass();
        }

        if (activePlayers.Count == 1)
        {
            StartCoroutine(GameOverWin(activePlayers[0]));
            StartHourGlass();
        }

        if (GameActive)
        {
            Timer -= Time.deltaTime;
            if (Timer <= 10)
            {
                TimerDisplay.GetComponent<TweenScaleBehaviour>().TweenScale();
            }

            var slope = 1f / 60f;
            var sliderAmount = slope * Timer;
            CountdownImage.fillAmount = sliderAmount;
            foreach (var playerStatBehaviour in players)
            {
                if (activePlayers.Contains(playerStatBehaviour))
                {
                    if (playerStatBehaviour.gameObject.transform.GetChild(0).GetComponent<PlayerStatBehaviour>()
                            .Health <= 0)
                    {
                        deathTimer += Time.deltaTime;

                        if (deathTimer >= 3f)
                        {
                            playerStatBehaviour.gameObject.transform.position = new Vector3(playerStatBehaviour.gameObject.transform.position.x, playerStatBehaviour.gameObject.transform.position.y - 10f,
                                playerStatBehaviour.gameObject.transform.position.z);

                            var audioSource = playerStatBehaviour.GetComponent<AudioSource>();
                            audioSource.PlayOneShot(audioSource.clip);

                            activePlayers.Remove(playerStatBehaviour);
                            playerStatBehaviour.gameObject.transform.GetComponentInChildren<Camera>().enabled = false;
                            var cam = players.IndexOf(playerStatBehaviour);
                            if (SpecateCameras[cam].enabled)
                                return;
                            SpecateCameras[cam].gameObject.SetActive(true);
                            SpecateCameras[cam].enabled = true;
                            SpecateCameras[cam].gameObject.transform.position = Vector3.zero;

                            SpecateCameras[cam].gameObject.GetComponentInChildren<Camera>().rect = playerStatBehaviour
                                .gameObject
                                .transform
                                .GetComponentInChildren<Camera>().rect;

                            StartCoroutine(CycleSpecate(SpecateCameras[cam].gameObject));
                            deathTimer = 0;
                        }
                    }
                }
            }
        }
        else
        {
            foreach (var player in players)
            {
                player.GetComponent<AudioSource>().Pause();
            }
        }
    }

    private void StartHourGlass()
    {
        HourGlass.enabled = true;
        testTimer -= Time.deltaTime;
        var slope = 1f / 10f;
        var sliderAmount = slope * testTimer;
        HourGlass.fillAmount = sliderAmount;
    }

    private IEnumerator GameOverLost()
    {
        GameActive = false;
        foreach (var outline in FindObjectsOfType<Outline>())
        {
            outline.gameObject.SetActive(false);
        }
        foreach (var specateCamera in SpecateCameras) specateCamera.gameObject.SetActive(false);
        foreach (var player in players)
        {
            if (player == null) break;
            var cam = player.gameObject.GetComponentInChildren<Camera>();
            if (!cam.enabled) cam.enabled = true;
        }

        foreach (var WarpUI in GameObject.FindGameObjectsWithTag("WarpUI")) WarpUI.gameObject.SetActive(false);
        foreach (var select in GameObject.FindGameObjectsWithTag("Select")) select.gameObject.SetActive(false);
        foreach (var Over in GameObject.FindGameObjectsWithTag("GameOver"))
        {
            Over.GetComponent<Image>().enabled = true;
            Over.GetComponentInChildren<Text>().enabled = true;
        }

        TimerDisplay.text = 0.ToString();
        yield return new WaitForSeconds(GameOverScreenTimer);
        SceneManager.LoadScene("104.FullGame");
    }

    private IEnumerator GameOverWin(GameObject player)
    {
        GameActive = false;
        BackGround.SetActive(true);
        foreach (var outline in FindObjectsOfType<Outline>())
        {
            outline.gameObject.SetActive(false);
        }
        foreach (var playerStatBehaviour in FindObjectsOfType<PlayerStatBehaviour>())
        {
            if (playerStatBehaviour.gameObject.transform.parent.GetComponentInChildren<Camera>().tag == "MainCamera")
                playerStatBehaviour.gameObject.transform.parent.GetComponentInChildren<Camera>().enabled = true;
        }

        DisplayWinner.enabled = true;

        foreach (var child in activePlayers[0].GetComponentsInChildren<Transform>())
        {
            if (child.tag == "SecondCamera")
            {
                child.GetComponent<Camera>().enabled = true;
                DisplayWinner.texture = TargeTexture;
                child.GetComponent<Camera>().targetTexture = TargeTexture;
            }
        }
        //if (activePlayers[0].gameObject.GetComponentInChildren<Camera>().transform.tag == "SecondCamera")
        //{
        //    activePlayers[0].gameObject.GetComponentInChildren<Camera>().enabled = true;
        //    activePlayers[0].gameObject.GetComponentInChildren<Camera>().targetTexture = TargeTexture;
        //}

        foreach (var WarpUI in GameObject.FindGameObjectsWithTag("WarpUI")) WarpUI.gameObject.SetActive(false);
        foreach (var select in GameObject.FindGameObjectsWithTag("Select")) select.gameObject.SetActive(false);
        foreach (var Over in GameObject.FindGameObjectsWithTag("GameOver"))
        {
            Over.GetComponent<Image>().enabled = true;
            Over.GetComponentInChildren<Text>().enabled = true;
            Over.GetComponentInChildren<Text>().text = "Winner: " + player.name;
        }

        TimerDisplay.text = 0.ToString();
        yield return new WaitForSeconds(GameOverScreenTimer);
        SceneManager.LoadScene("104.FullGame");
    }

    private IEnumerator CycleSpecate(GameObject camera)
    {
        var i = 0;
        while (GameActive)
        {
            var currentPlayerCam = activePlayers[i].gameObject.GetComponentInChildren<Camera>();
            camera.gameObject.transform.SetParent(currentPlayerCam.transform.parent);
            camera.gameObject.transform.position = currentPlayerCam.transform.position;
            camera.gameObject.transform.rotation = currentPlayerCam.transform.rotation;
            yield return new WaitForSeconds(3);
            i++;
            if (i == activePlayers.Count)
                i = 0;
        }
    }
#if UNITY_EDITOR
    [ContextMenu("KillAllButOneRandom")]
    public void KillAllButOne()
    {
        var playersNumbers = new List<string>() {"P1", "P2", "P3", "P4"};
        foreach (var activePlayer in players)
        {
            if (activePlayer.transform.GetChild(0).tag != playersNumbers[Random.Range(0, 3)])
                activePlayer.GetComponentInChildren<PlayerStatBehaviour>().Health = 0;
        }
    }
    [ContextMenu("KillP1")]
    public void KillP1()
    {
        foreach (var activePlayer in activePlayers)
        {
           if (activePlayer.transform.GetChild(0).tag == "P1")
               activePlayer.GetComponentInChildren<PlayerStatBehaviour>().Health = 0;
        }
    }
    [ContextMenu("KillP2")]
    public void KillP2()
    {
        foreach (var activePlayer in activePlayers)
        {
            if (activePlayer.transform.GetChild(0).tag == "P2")
                activePlayer.GetComponentInChildren<PlayerStatBehaviour>().Health = 0;
        }
    }
    [ContextMenu("KillP3")]
    public void KillP3()
    {
        foreach (var activePlayer in activePlayers)
        {
            if (activePlayer.transform.GetChild(0).tag == "P3")
                activePlayer.GetComponentInChildren<PlayerStatBehaviour>().Health = 0;
        }
    }
    [ContextMenu("KillP4")]
    public void KillP4()
    {
        foreach (var activePlayer in activePlayers)
        {
            if (activePlayer.transform.GetChild(0).tag == "P4")
                activePlayer.GetComponentInChildren<PlayerStatBehaviour>().Health = 0;
        }
    }
#endif
}
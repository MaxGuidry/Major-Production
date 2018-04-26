using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    [SerializeField] private List<GameObject> activePlayers;

    private bool GameActive;
    public GameObject GameOverUI;
    public List<GameObject> players;
    public List<Camera> SpecateCameras;
    public float Timer = 60f;

    public Text TimerDisplay;

    // Use this for initialization
    private void Start()
    {
        GameActive = true;
        activePlayers = new List<GameObject>();
        //foreach (var playerStatBehaviour in FindObjectsOfType<PlayerStatBehaviour>())
        //{
        //    if (playerStatBehaviour.Health != 0)
        //        players.Add(playerStatBehaviour.gameObject);
        //}

        //var temp = players[2];
        //players[2] = players[3];
        //players[3] = temp;
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
        Timer -= Time.deltaTime;
        if (Timer >= 0f)
            TimerDisplay.text = Mathf.Round(Timer).ToString();
        if (players.Count == 0)
            return;
        if (Timer <= 0)
        {
            foreach (var player in players) player.GetComponentInChildren<CharacterMovement>().enabled = false;
            //players.Remove(playerStatBehaviour.gameObject);
            StartCoroutine(GameOverLost());
        }

        if (activePlayers.Count == 1) StartCoroutine(GameOverWin(activePlayers[0]));
        //else if (players.Count == 1)
        //{
        //    foreach (var player in players)
        //    {
        //        var playa = player.GetComponentInChildren<PlayerStatBehaviour>();
        //    }
        //    StartCoroutine(GameOverWin(players[0]));
        //}

        if (GameActive)
        {
            foreach (var playerStatBehaviour in players)
            {
                if (activePlayers.Contains(playerStatBehaviour))
                {
                    if (playerStatBehaviour.gameObject.transform.GetChild(0).GetComponent<PlayerStatBehaviour>()
                            .Health <= 0)
                    {
                        activePlayers.Remove(playerStatBehaviour);
                        //playerStatBehaviour.gameObject.SetActive(false);
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
                        //cam.gameObject.transform.RotateAround(playerStatBehaviour.gameObject.transform.localPosition, playerStatBehaviour.gameObject.transform.localPosition, 5f);

                        StartCoroutine(CycleSpecate(SpecateCameras[cam].gameObject));
                    }
                }
            }
        }
    }

    private IEnumerator GameOverLost()
    {
        GameActive = false;
        foreach (var specateCamera in SpecateCameras) specateCamera.gameObject.SetActive(false);
        foreach (var player in players)
        {
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
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("104.FullGame");
    }

    private IEnumerator GameOverWin(GameObject player)
    {
        GameActive = false;
        foreach (var playerStatBehaviour in FindObjectsOfType<PlayerStatBehaviour>())
            playerStatBehaviour.gameObject.transform.parent.GetComponentInChildren<Camera>().enabled = true;
        foreach (var WarpUI in GameObject.FindGameObjectsWithTag("WarpUI")) WarpUI.gameObject.SetActive(false);
        foreach (var select in GameObject.FindGameObjectsWithTag("Select")) select.gameObject.SetActive(false);
        foreach (var Over in GameObject.FindGameObjectsWithTag("GameOver"))
        {
            Over.GetComponent<Image>().enabled = true;
            Over.GetComponentInChildren<Text>().enabled = true;
            Over.GetComponentInChildren<Text>().text = "Winner: " + player.name;
        }

        TimerDisplay.text = 0.ToString();
        yield return new WaitForSeconds(3);
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
            //camera.gameObject.transform.position += new Vector3(0, 20, 0);
            //camera.gameObject.transform.LookAt(activePlayers[i].gameObject.transform.GetChild(0).transform);
            //camera.gameObject.transform.localRotation = currentPlayerCam.gameObject.transform.localRotation;


            //camera.gameObject.transform.localPosition = new Vector3(
            //    currentPlayerCam.gameObject.transform
            //        .localPosition.x + 20,
            //    currentPlayerCam.gameObject.transform
            //        .localPosition.y + 20,
            //    currentPlayerCam.gameObject.transform
            //        .localPosition.z - 20);


            yield return new WaitForSeconds(3);
            i++;
            if (i == activePlayers.Count)
                i = 0;
        }
    }
}
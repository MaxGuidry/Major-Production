using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountDown : MonoBehaviour
{
    public float Timer = 60f;
    public GameObject GameOverUI;
    public Text TimerDisplay;
    public List<GameObject> players;
    public List<Camera> SpecateCameras;

    private bool GameActive;
    // Use this for initialization
    void Start()
    {
        GameActive = true;
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
    }

    // Update is called once per frame
    void Update()
    {
        Timer -= Time.deltaTime;
        if (Timer >= 0f)
            TimerDisplay.text = Mathf.Round(Timer).ToString();
        foreach (var playerStatBehaviour in players)
        {
            if (playerStatBehaviour.GetComponentInChildren<PlayerStatBehaviour>().Health <= 0)
            {
                //playerStatBehaviour.gameObject.SetActive(false);
                playerStatBehaviour.gameObject.transform.GetComponentInChildren<Camera>().enabled = false;
                var cam = players.IndexOf(playerStatBehaviour);
                if (SpecateCameras[cam].enabled)
                    return;
                SpecateCameras[cam].enabled = true;
                SpecateCameras[cam].gameObject.transform.position = Vector3.zero;
                SpecateCameras[cam].gameObject.SetActive(true);
                SpecateCameras[cam].enabled = true;
                SpecateCameras[cam].gameObject.GetComponentInChildren<Camera>().rect = playerStatBehaviour.gameObject.transform
                    .GetComponentInChildren<Camera>().rect;
                //cam.gameObject.transform.RotateAround(playerStatBehaviour.gameObject.transform.localPosition, playerStatBehaviour.gameObject.transform.localPosition, 5f);

                StartCoroutine(CycleSpecate(SpecateCameras[cam].gameObject));
            }

            if (Timer <= 0)
            {
                foreach (var player in players)
                {
                    player.gameObject.SetActive(false);
                }
                players.Remove(playerStatBehaviour.gameObject);
                StartCoroutine(GameOverLost());
            }
            else if (players.Count == 1)
            {
                StartCoroutine(GameOverWin(players[0]));
            }
        }
    }

    private IEnumerator GameOverLost()
    {
        GameActive = false;
        foreach (var playerStatBehaviour in FindObjectsOfType<PlayerStatBehaviour>())
        {
            playerStatBehaviour.gameObject.transform.parent.GetComponentInChildren<Camera>().enabled = true;
        }

        foreach (var WarpUI in GameObject.FindGameObjectsWithTag("WarpUI"))
        {
            WarpUI.gameObject.SetActive(false);
        }
        foreach (var select in GameObject.FindGameObjectsWithTag("Select"))
        {
            select.gameObject.SetActive(false);
        }
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
        {
            playerStatBehaviour.gameObject.transform.parent.GetComponentInChildren<Camera>().enabled = true;
        }
        foreach (var WarpUI in GameObject.FindGameObjectsWithTag("WarpUI"))
        {
            WarpUI.gameObject.SetActive(false);
        }
        foreach (var select in GameObject.FindGameObjectsWithTag("Select"))
        {
            select.gameObject.SetActive(false);
        }
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
            if (players[i].gameObject.transform.GetChild(0).GetComponentInChildren<PlayerStatBehaviour>().Health > 0)
            {
                camera.gameObject.transform.position = new Vector3(
                    players[i].gameObject.transform.GetChild(0).GetComponentInChildren<PlayerStatBehaviour>().transform
                        .position.x + 10,
                    players[i].gameObject.transform.GetChild(0).GetComponentInChildren<PlayerStatBehaviour>().transform
                        .position.y + 10,
                    players[i].gameObject.transform.GetChild(0).GetComponentInChildren<PlayerStatBehaviour>().transform
                        .position.z - 10);
                camera.gameObject.transform.LookAt(players[i].gameObject.transform);
                yield return new WaitForSeconds(3);
                i++;
                if (i == players.Count)
                    i = 0;
            }
            else
            {
                yield return new WaitForSeconds(0);
                i++;
                if (i == players.Count)
                    i = 0;
            }
        }
    }
}

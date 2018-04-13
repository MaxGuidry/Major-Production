using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerSpawner : MonoBehaviour {
    public SplitScreenPlayers PlayerAmount;
    public List<GameObject> Players;
    // Use this for initialization
    void Awake () {
        switch (PlayerAmount.PlayersJoined)
        {
            case 0:
                SceneManager.LoadScene("103.MainMenu");
                break;
            case 1:
                SceneManager.LoadScene("103.MainMenu");
                break;
            case 2:
                Players[0].SetActive(true);
                Players[1].SetActive(true);

                Players[0].gameObject.GetComponentInChildren<Camera>().rect = new Rect(.25f, .5f, .5f, 1);
                Players[1].gameObject.GetComponentInChildren<Camera>().rect = new Rect(.25f, -.5f, .5f, 1);
                break;
            case 3:
                Players[0].SetActive(true);
                Players[1].SetActive(true);
                Players[2].SetActive(true);
                Players[2].gameObject.transform.GetComponentInChildren<PlayerStatBehaviour>().gameObject.SetActive(true);

                Players[0].gameObject.GetComponentInChildren<Camera>().rect = new Rect(0, .5f, .5f, 1);
                Players[1].gameObject.GetComponentInChildren<Camera>().rect = new Rect(.5f, .5f, 1, 1);
                Players[2].gameObject.GetComponentInChildren<Camera>().rect = new Rect(0.25f, 0, .5f, .5f);
                break;
            case 4:
                foreach (var player in Players)
                    player.SetActive(true);
                Players[0].gameObject.GetComponentInChildren<Camera>().rect = new Rect(0, .5f, .5f, 1);
                Players[1].gameObject.GetComponentInChildren<Camera>().rect = new Rect(.5f, .5f, 1, 1);
                Players[2].gameObject.GetComponentInChildren<Camera>().rect = new Rect(0, 0, .5f, .5f);
                Players[3].gameObject.GetComponentInChildren<Camera>().rect = new Rect(.5f, 0, .5f, .5f);
                break;
            default:
                SceneManager.LoadScene("103.MainMenu");
                break;
        }
    }
}

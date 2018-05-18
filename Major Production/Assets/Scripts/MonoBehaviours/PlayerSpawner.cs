using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerSpawner : MonoBehaviour
{
    public SplitScreenPlayers PlayerAmount;
    public List<GameObject> Players;
    // Use this for initialization
    void Awake()
    {
        foreach (var player in Players)
        {
            foreach (var child in player.GetComponentsInChildren<Transform>())
            {
                if (child.tag == "MainCamera")
                {
                    child.GetComponent<Camera>().enabled = false;
                    child.GetComponent<Camera>().enabled = true;
                }
            }
        }

        Players[0].gameObject.GetComponentInChildren<Camera>().rect = new Rect(0, .5f, .5f, 1);
        Players[0].transform.position = new Vector3(26, 31, -210);
        Players[1].gameObject.GetComponentInChildren<Camera>().rect = new Rect(.5f, .5f, 1, 1);
        Players[1].transform.position = new Vector3(108, 32, -212);
        Players[2].gameObject.GetComponentInChildren<Camera>().rect = new Rect(0, 0, .5f, .5f);
        Players[2].transform.position = new Vector3(18, 26, -289);
        Players[3].gameObject.GetComponentInChildren<Camera>().rect = new Rect(.5f, 0, .5f, .5f);
        Players[3].transform.position = new Vector3(109, 33, -302);
        //switch (PlayerAmount.PlayersJoined)
        //{
        //    case 0:
        //        SceneManager.LoadScene("103.MainMenu");
        //        break;
        //    case 1:
        //        SceneManager.LoadScene("103.MainMenu");
        //        break;
        //    case 2:
        //        Players[0].SetActive(true);
        //        Players[1].SetActive(true);

        //        Players[0].gameObject.GetComponentInChildren<Camera>().rect = new Rect(.25f, .5f, .5f, 1);
        //        Players[1].gameObject.GetComponentInChildren<Camera>().rect = new Rect(.25f, -.5f, .5f, 1);
        //        break;
        //    case 3:
        //        Players[0].SetActive(true);
        //        Players[1].SetActive(true);
        //        Players[2].SetActive(true);
        //        Players[2].gameObject.transform.GetComponentInChildren<PlayerStatBehaviour>().gameObject.SetActive(true);

        //        Players[0].gameObject.GetComponentInChildren<Camera>().rect = new Rect(0, .5f, .5f, 1);
        //        Players[1].gameObject.GetComponentInChildren<Camera>().rect = new Rect(.5f, .5f, 1, 1);
        //        Players[2].gameObject.GetComponentInChildren<Camera>().rect = new Rect(0.25f, 0, .5f, .5f);
        //        break;
        //    case 4:
        //        foreach (var player in Players)
        //            player.SetActive(true);
        //        //Players[0].gameObject.GetComponentInChildren<Camera>().rect = new Rect(0, .5f, .5f, 1);
        //        Players[0].transform.position = new Vector3(26, 31, -210);
        //        //Players[1].gameObject.GetComponentInChildren<Camera>().rect = new Rect(.5f, .5f, 1, 1);
        //        Players[1].transform.position = new Vector3(108, 32, -212);
        //        //Players[2].gameObject.GetComponentInChildren<Camera>().rect = new Rect(0, 0, .5f, .5f);
        //        Players[2].transform.position = new Vector3(18, 26, -289);
        //        //Players[3].gameObject.GetComponentInChildren<Camera>().rect = new Rect(.5f, 0, .5f, .5f);
        //        Players[3].transform.position = new Vector3(109, 33, -302);
        //        break;
        //    default:
        //        SceneManager.LoadScene("103.MainMenu");
        //        break;
        //}
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[CreateAssetMenu]
public class GameManagerBehaviour : ScriptableObject {

    public static void RestartGame()
    {
        SceneManager.LoadScene("100.Inventory");
    }
}

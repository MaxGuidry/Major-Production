using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDestroyBehaviour : MonoBehaviour {

    void OnEnable()
    {
        Invoke("Destroy", 1f);
    }

    void Destroy()
    {
        gameObject.SetActive(false);
    }

    void OnDisable()
    {
        CancelInvoke();
    }
}

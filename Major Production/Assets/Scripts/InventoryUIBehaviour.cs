using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

public class InventoryUIBehaviour : MonoBehaviour {

    private InventoryBehaviour test;
    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
	    test.InvChange.Invoke(UpdateUI());
    }

    public Inventory UpdateUI()
    {
        Debug.Log("Update UI");
        return test.inventory;
    }
}

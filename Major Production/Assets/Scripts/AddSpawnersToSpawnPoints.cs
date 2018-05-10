using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSpawnersToSpawnPoints : MonoBehaviour {

	// Use this for initialization
    public GameObject TeleporterPrefab;
	void Start () {
	    var children =gameObject.transform.GetComponentsInChildren<Transform>();
	    List<GameObject> spawnPoints = new List<GameObject>();
	    foreach (var child in children)
	    {
	        if (child.gameObject.tag != "SpawnPoint")
	            continue;
	        spawnPoints.Add(child.gameObject);
	    }

	    foreach (var spawnPoint in spawnPoints)
	    {
	        var go = GameObject.Instantiate(TeleporterPrefab,spawnPoint.transform.position,spawnPoint.transform.rotation,spawnPoint.transform);

	    }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

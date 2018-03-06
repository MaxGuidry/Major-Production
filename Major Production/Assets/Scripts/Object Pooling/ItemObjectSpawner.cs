using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObjectSpawner : MonoBehaviour
{
    public ItemObjectPooler Pooler;
    public GameObject ObjectToSpawnOn;

    public GameEventArgs ItemPickedUp;
    // Use this for initialization
    void Start()
    {
        for (var i = 0; i < Pooler.PooledObjects.Count; i ++)
        {
            SpawnItemOnSphere();
        }
    }

    private void SpawnItemOnSphere()
    {
        var obj = ItemObjectPooler.current.GetPooledGameObject();
        obj.GetComponent<ItemInteractionBehaviour>().ItemPickedUp = ItemPickedUp;
        if (obj == null) return;

        var spawnPos = Random.onUnitSphere * ((ObjectToSpawnOn.transform.localScale.x / 2) + obj.transform.localScale.y * 0.5f) + ObjectToSpawnOn.transform.position;

        var spawnRot = Quaternion.identity;

        obj.transform.position = spawnPos;
        obj.transform.rotation = spawnRot;
        obj.transform.LookAt(obj.transform);
        obj.transform.Rotate(-90, 0, 0);
        obj.SetActive(true);
    }
}
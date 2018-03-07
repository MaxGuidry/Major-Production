using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObjectSpawner : MonoBehaviour
{
    public ItemObjectPooler Pooler;
    public GameObject ObjectToSpawnOn;
    public GameEventArgs ItemPickedUp;

    private GameObject _obj;
    private Vector3 _spawnPos;
    // Use this for initialization
    private void Start()
    {
        for (var i = 0; i <= Pooler.PooledAmount; i++)
        {
            SpawnItemOnSphere();
        }
    }

    /// <summary>
    /// Sets the GameEventArgs of the object being spawned
    /// Calls the create function from the ItemObjectPooler class
    /// </summary>
    private void SpawnItemOnSphere()
    {
        _obj = ItemObjectPooler.current.GetPooledGameObject();
        _spawnPos = Random.onUnitSphere * (ObjectToSpawnOn.transform.localScale.x / 2 + _obj.transform.localScale.y * 0.5f) + ObjectToSpawnOn.transform.position;
        _obj.GetComponent<ItemInteractionBehaviour>().ItemPickedUp = ItemPickedUp;
        Pooler.Create(_obj, _spawnPos, Quaternion.identity);
    }
}
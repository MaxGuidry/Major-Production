using UnityEngine;

public class ItemObjectSpawner : MonoBehaviour
{
    public GameObject ObjectToSpawnOn;
    private GameObject _obj;
    private Vector3 _spawnPos;
    // Use this for initialization
    private void Start()
    {
        for (var i = 0; i <= ItemObjectPooler.s_instance.PooledAmount; i++)
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
        _obj = ItemObjectPooler.s_instance.GetPooledGameObject();
        _spawnPos = Random.onUnitSphere * (150 / 2 + 150 * 0.5f) + ObjectToSpawnOn.transform.position;
        ItemObjectPooler.s_instance.Create(_obj, _spawnPos, Quaternion.identity);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [Serializable]
    public class Pool
    {
        public string Name;
        public GameObject Item;
    }

    public List<Pool> Pools;
    private int iD;
    public Dictionary<string, object> GenericObjectPool = new Dictionary<string, object>();

    public void AddToPool(string ItemName, GameObject ItemObject)
    {
        var itemPool = new Pool
        {
            Name = ItemName,
            Item = ItemObject,
        };

        Pools.Add(itemPool);
        iD++;
        itemPool.Name += iD;
        ItemObject.gameObject.SetActive(false);
        GenericObjectPool.Add(itemPool.Name, ItemObject);
    }

    public void DropFromPool(string Name, Vector3 pos, Quaternion rot)
    {
        if (!GenericObjectPool.ContainsKey(Name))
            return;

        var objToSpawn = GenericObjectPool[Name] as GameObject;
        if (objToSpawn == null) return;

        objToSpawn.SetActive(true);
        objToSpawn.transform.position = pos;
        objToSpawn.transform.rotation = rot;
    }
}

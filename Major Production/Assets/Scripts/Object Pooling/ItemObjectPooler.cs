using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObjectPooler : MonoBehaviour
{
    public static ItemObjectPooler current;
    public GameObject PooledObject;
    public int PooledAmount;
    public bool willGrow = true;
    public List<GameObject> PooledObjects;

    public List<GameObject> RandomGameObjects;

    void Awake()
    {
        current = this;
        PooledObjects = new List<GameObject>();
        for (var i = 0; i < PooledAmount; i++)
        {
            PooledObject = RandomGameObjects[Random.Range(0, RandomGameObjects.Count)];
            var obj = Instantiate(PooledObject);
            obj.SetActive(false);
            obj.transform.SetParent(gameObject.transform);
            PooledObjects.Add(obj);
        }
    }

    /// <summary>
    /// Cant figure out how to use properly
    /// </summary>
    /// <returns></returns>
    public GameObject GetPooledGameObject()
    {
        foreach (var obj in PooledObjects)
            if (!obj.activeInHierarchy)
                return obj;
        if (willGrow)
        {
            var newObj = Instantiate(PooledObject);
            PooledObjects.Add(newObj);
            return newObj;
        }
        return null;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObjectPooler : MonoBehaviour
{
    public static ItemObjectPooler current;
    public GameObject PooledObject;
    public int PooledAmount;
    //public bool willGrow = true;
    public List<GameObject> PooledObjects;
    public List<GameObject> RandomGameObjects;

    private int activeCount;

    private void Awake()
    {
        current = this;
        PooledObjects = new List<GameObject>();
        for (var i = 0; i <= PooledAmount; i++)
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
        //if (willGrow)
        //{
        //    var newObj = Instantiate(PooledObject);
        //    PooledObjects.Add(newObj);
        //    return newObj;
        //}
        return null;
    }

    /// <summary>
    /// LifeCyle of create (1: activeCount greater than or equal to PooledAmount 2: activeCount Less than PooledAmount)
    /// Create --1> AddToPool --1> Remove From Pool // Create --2> RemoveFromPool
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="objPos"></param>
    /// <param name="rot"></param>
    public void Create(GameObject prefab, Vector3 objPos, Quaternion rot)
    {
        activeCount++;
        if (activeCount < PooledAmount)
        {
            RemoveFromPool(prefab);
        }
        else if (activeCount >= PooledAmount)
        {
            var newObj = Instantiate(prefab, objPos, rot);
            AddToPool(newObj);
            newObj.transform.SetParent(gameObject.transform);
        }
        prefab.transform.position = objPos;
        prefab.transform.rotation = rot;
    }

    /// <summary>
    /// End User use case
    /// </summary>
    /// <param name="prefab"></param>
    public void Destroy(GameObject prefab)
    {
        AddToPool(prefab);
    }

    /// <summary>
    /// Removes the object from pool by setting it active to true
    /// then removing that object from the list
    /// </summary>
    /// <param name="prefab"></param>
    private void RemoveFromPool(GameObject prefab)
    {
        prefab.SetActive(true);
        PooledObjects.Remove(prefab);
    }

    /// <summary>
    /// Adds item to pool by setting active to false 
    /// then adds that object to the list
    /// Also checks if case number one and if so calls remove from pool
    /// </summary>
    /// <param name="prefab"></param>
    private void AddToPool(GameObject prefab)
    {
        prefab.SetActive(false);
        PooledObjects.Add(prefab);
    }
}
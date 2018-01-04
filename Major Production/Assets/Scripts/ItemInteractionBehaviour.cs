using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjects;

public class ItemInteractionBehaviour : MonoBehaviour
{
    public Item item;
    private GameObject _player;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    if (_player.GetComponent<InventoryBehaviour>().ActiveInventory.Count == 0)
        //        return;
        //    else if (_player.GetComponent<InventoryBehaviour>().ActiveInventory.Count == 1)
        //    {
        //        _player.GetComponent<InventoryBehaviour>().RemoveFromInventory(item);
        //        GameObject.CreatePrimitive(PrimitiveType.Cube);
        //    }
        //    else
        //    {
        //        _player.GetComponent<InventoryBehaviour>().RemoveFromInventory(
        //            _player.GetComponent<InventoryBehaviour>().ActiveInventory[
        //                _player.GetComponent<InventoryBehaviour>().ActiveInventory.Count - 1]);
        //        GameObject.CreatePrimitive(PrimitiveType.Cube);
        //    }
        //}
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        other.GetComponent<InventoryBehaviour>().AddToInventory(item);
        Destroy(gameObject);
    }
}

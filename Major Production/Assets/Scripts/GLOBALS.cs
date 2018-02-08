using UnityEngine;
using UnityEngine.Events;
using System;
using ScriptableObjects;

[Serializable]
public class InventoryEvent : UnityEvent<Inventory> { }


[Serializable]
public class CollisionEvent : UnityEvent<Item> { }


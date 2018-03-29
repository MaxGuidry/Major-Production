using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
public class ReachInteractionBehaviour : MonoBehaviour
{    
    public GameEventArgs ReachedCheckPoint;
    public ScriptableObjects.Item ReachItem;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            ReachedCheckPoint.Raise(ReachItem);
            Destroy(gameObject);
        }
    }
}

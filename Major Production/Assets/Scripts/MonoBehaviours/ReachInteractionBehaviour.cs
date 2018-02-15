using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
public class ReachInteractionBehaviour : MonoBehaviour
{
    public GameEventArgs ReachedCheckPoint;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            ReachedCheckPoint.Raise(other.gameObject);
            Destroy(gameObject);
        }
    }
}

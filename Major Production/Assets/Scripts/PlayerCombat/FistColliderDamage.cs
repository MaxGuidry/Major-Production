using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistColliderDamage : MonoBehaviour
{
    

    public CharacterMovement player;
    //public PlayerStatBehaviour stats;
	// Use this for initialization


    void OnTriggerEnter(Collider other)
    {
        if (player.state == CharacterMovement.PlayerState.Attacking)
        {
            var idamageable = other.gameObject.GetComponent<IDamageable>();
            if (idamageable != null)
            {
                idamageable.TakeDamage(50);
            }
        }
    }

}

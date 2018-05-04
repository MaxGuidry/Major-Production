using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistColliderDamage : MonoBehaviour
{
    

    public CharacterMovement player;
    //public PlayerStatBehaviour stats;
	// Use this for initialization

    private bool DidHitDuringThisAnimation = false;
    void OnTriggerEnter(Collider other)
    {
        if (DidHitDuringThisAnimation)
            return;
        if (player.state == CharacterMovement.PlayerState.Attacking)
        {
            var idamageable = other.gameObject.GetComponent<IDamageable>();
            if (idamageable != null)
            {
                idamageable.TakeDamage(player.gameObject.GetComponent<PlayerStatBehaviour>().Damage);
                DidHitDuringThisAnimation = true;
            }
        }
    }

    void Update()
    {
        if (player.state != CharacterMovement.PlayerState.Attacking)
            DidHitDuringThisAnimation = false;
    }
}

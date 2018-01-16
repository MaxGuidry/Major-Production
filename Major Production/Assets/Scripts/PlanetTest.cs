using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetTest : MonoBehaviour
{
    public float gravity;

    public void Attract(Transform player)
    {
        var gravityUp = (player.position - transform.position).normalized;
        var localUp = player.up;

        player.GetComponent<Rigidbody>().AddForce(gravityUp * gravity);

        var targetRotation = Quaternion.FromToRotation(localUp, gravityUp) * player.rotation;

        player.rotation = Quaternion.Slerp(player.rotation, targetRotation, 50f * Time.deltaTime);
    }
}

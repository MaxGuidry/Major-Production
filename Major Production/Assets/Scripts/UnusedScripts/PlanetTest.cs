using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetTest : MonoBehaviour
{
    public float gravity = -9.8f;

    public void Attract(Rigidbody player)
    {
        var gravityUp = (player.position - transform.position).normalized;
        var localUp = player.transform.up;

        // Apply downwards gravity to body
        player.AddForce(gravityUp * gravity);
        // Allign bodies up axis with the centre of planet
        player.rotation = Quaternion.FromToRotation(localUp, gravityUp) * player.rotation;
    }
}

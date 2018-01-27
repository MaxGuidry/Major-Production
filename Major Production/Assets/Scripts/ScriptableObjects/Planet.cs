using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Planet", menuName = "Planet", order = 0)]
public class Planet : ScriptableObject
{

    public float gravity;

    public Vector3 center;

    public float mass;
    public float radius;
    //public PlanetType environment;
    public void Initialize(Vector3 Center, float Radius,float Mass)
    {
        center = Center;
        radius = Radius;
        mass = Mass;
    }
}

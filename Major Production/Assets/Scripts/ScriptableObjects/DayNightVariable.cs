using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DayNight", menuName = "DayNight")]
public class DayNightVariable : ScriptableObject
{
    public float Time;
    public float DayLength;
    public Light DirectionalLight;
    public float MaxLightIntensity;
}

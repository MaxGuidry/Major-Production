using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DayNightCycleBehaviour : MonoBehaviour
{
    public DayNightVariable Cycle;
    // Use this for initialization
    void Start()
    {
        Cycle.Time = 0;
        Cycle.DirectionalLight = GameObject.FindGameObjectWithTag("Sun").GetComponent<Light>();
        Cycle.MaxLightIntensity = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Cycle.Time += Time.deltaTime;

        //Rotate the Light as the day goes on.
        var degrees = (Cycle.Time / Cycle.DayLength) * 180;
        Cycle.DirectionalLight.transform.rotation = Quaternion.Euler(new Vector3(degrees, 330, 0));
        //Get darker into the night
        if (Cycle.Time > Cycle.DayLength)
        {
            var nightamount = (Cycle.Time - Cycle.DayLength) / Cycle.DayLength;
            if (nightamount > 0 && nightamount < 0.1f)
                Cycle.DirectionalLight.intensity = (1 - nightamount * 10) * Cycle.MaxLightIntensity;
            else if (nightamount > 0.9f && nightamount < 1)
                Cycle.DirectionalLight.intensity = (nightamount - 0.9f) * 10 * Cycle.MaxLightIntensity;
        }
        //Reset Light
        Cycle.Time = Cycle.Time % (Cycle.DayLength * 2);
    }
}

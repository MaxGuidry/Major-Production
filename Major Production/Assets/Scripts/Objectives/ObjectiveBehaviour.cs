using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[RequireComponent(typeof(AudioSource))]
public class ObjectiveBehaviour : MonoBehaviour
{
    public Objective Obj;
    private void OnEnable()
    {
        switch (Obj.MissionType)
        {
            case ObjectiveType.Gather:
                this.tag = "Gather";
                break;
            case ObjectiveType.Reach:
                this.tag = "Reach";
                break;
        }
    }
    private void Start()
    {
        this.name = Obj.Title;
        //Obj.testAudio = GetComponent<AudioSource>();
    }
    public void Update()
    {
        if (Obj.Status == ObjectiveStatus.Complete)
            Destroy(this.gameObject);
    }
}

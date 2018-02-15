using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[RequireComponent(typeof(AudioSource))]
public class ObjectiveCollider : MonoBehaviour
{
    public Objective Obj;
    private void OnEnable()
    {
        if (Obj.MissionType == ObjectiveType.Gather)
            this.tag = "Gather";
        else if (Obj.MissionType == ObjectiveType.Reach)
            this.tag = "Reach";
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

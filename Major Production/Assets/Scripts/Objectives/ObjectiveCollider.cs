using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveCollider : MonoBehaviour {
    public Objective Obj;
    private void OnEnable()
    {
        this.tag = "Objective";
    }
    private void Start()
    {
        this.name = Obj.Title;
    }
    public void Update()
    {
        if (Obj.Status == Objective.ObjectiveStatus.Complete)
            Destroy(this.gameObject);
    }
}

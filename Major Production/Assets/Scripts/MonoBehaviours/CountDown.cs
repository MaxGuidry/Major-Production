using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ActiveState : GLOBALS.IState
{
    public void OnEnter()
    {
        throw new System.NotImplementedException();
    }

    public void UpdateState(GLOBALS.IContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnExit()
    {
        throw new System.NotImplementedException();
    }
}

public class WinState : GLOBALS.IState
{
    public void OnEnter()
    {
        throw new System.NotImplementedException();
    }

    public void UpdateState(GLOBALS.IContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnExit()
    {
        throw new System.NotImplementedException();
    }
}

public class LoseState : GLOBALS.IState
{
    public void OnEnter()
    {
        throw new System.NotImplementedException();
    }

    public void UpdateState(GLOBALS.IContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnExit()
    {
        throw new System.NotImplementedException();
    }
}

public class CountDown : MonoBehaviour
{
    private GLOBALS.PlayerContext Context;
	// Use this for initialization
	void Start () {
		Context.Current = new ActiveState();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

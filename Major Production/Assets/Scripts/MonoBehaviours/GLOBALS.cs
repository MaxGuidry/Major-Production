using UnityEngine;
using UnityEngine.Events;
using System;
using ScriptableObjects;

[Serializable]
public class GameEventArgsResponse : UnityEvent<UnityEngine.Object[]>{}

public static class GLOBALS
{
    public static bool SplitscreenLocal;
    public static bool SplitscreenOnline;
    public static bool SoloOnline;

    public interface IState
    {
        void OnEnter();
        void UpdateState(IContext context);
        void OnExit();
    }
    public interface IContext
    {
        void ChangeState(IState next);
    }
    [System.Serializable]
    public class PlayerContext : IContext
    {
        public IState Current;
        public void ChangeState(IState next)
        {
            Current.OnExit();
            Current = next;
            Current.OnEnter();
        }
        public void UpdateContext()
        {
            Current.UpdateState(this);
        }
    }
}
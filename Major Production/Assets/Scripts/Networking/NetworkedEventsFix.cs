using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class NetworkedEventsFix : MonoBehaviour
{
    public GameEventArgsObject test;
    public CharacterMovement player;
    public void Start()
    {
        test = FindObjectOfType<InputEvents>().OnButtonPressed;
        player = this.GetComponent<CharacterMovement>();
        AssignPlayerEvents();
    }
    public void AssignPlayerEvents()
    {
        var testevent = new GameEventArgsListenerObject();
        testevent.ObjResponse = ScriptableObject.CreateInstance<GameEventArgsResponseObject>();
        testevent.ObjResponse.AddListener(player.Jump);
        testevent.ObjResponse.AddListener(player.HitObject);
        

        test.objListeners.Add(testevent);
    }
}

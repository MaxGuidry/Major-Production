//using System.Collections;
//using System.Collections.Generic;
//using ScriptableObjects;
//using UnityEngine;

//[CreateAssetMenu]
//public class QuestChain : ScriptableObject
//{
//    public List<Quest> quests;
//    public GameEventArgs QuestChainStarted;
//    public Quest current;
//    public int currentIndex = 0;

//    void OnEnable()
//    {
//        current = quests[currentIndex];
//    }

//    public void NextQuest(Object[] args)
//    {Debug.Log("quest completed");
//        var sender = args[0] as Quest;
//        if (quests[currentIndex] == sender)
//        {
//            currentIndex++;
//            current = quests[currentIndex];
//        }
//    }
//}

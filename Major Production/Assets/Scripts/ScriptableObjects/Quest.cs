using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ScriptableObjects
{
    public enum QuestProgess
    {
        Active = 0,
        Complete = 1
    }

    [CreateAssetMenu(fileName = "Quest", menuName = "Quest")]
    public class Quest : ScriptableObject
    {
        public string Title;
        public string Description;
        public QuestProgess Progess;
        public int CurrentAmount, RequiredAmount;
    }
}
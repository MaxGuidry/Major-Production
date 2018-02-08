using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ScriptableObjects
{
    public enum QuestProgess
    {
        None = 0,
        InActive = 1,
        Active = 2,
        Complete = 3
    }

    [CreateAssetMenu(fileName = "Quest", menuName = "Quest")]
    public class Quest : ScriptableObject
    {
        public string Title;
        public string Description;
        public QuestProgess Progess;
        public Item RequiredItem;
        public int startAmount = 0;
        public int currentAmount = 0;
        public int doneAmount = 5;
        
        public void ProgressQuest(ScriptableObject item)
        {
            if (item == RequiredItem)
            {
                currentAmount++;
            }
        }

        
    }
}
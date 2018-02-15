//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections;
//using System.Collections.Generic;

//namespace ScriptableObjects
//{
//    public enum QuestProgess
//    {
//        None = 0,
//        InActive = 1,
//        Active = 2,
//        Complete = 3
//    }

//    [CreateAssetMenu(fileName = "Quest", menuName = "Quest")]
//    public class Quest : ScriptableObject
//    {
//        public string Title;
//        public string Description;
//        public QuestProgess Progess;
//        public Item RequiredItem;
//        public int CurrentAmount = 0;
//        public int RequiredAmount = 5;
//        public GameEventArgs QuestStarted;
//        public GameEventArgs QuestEnded;
//        public GameEventArgs QuestChange;
        
//        public void ProgressQuest(UnityEngine.Object[] args)
//        {
//            var sender = args[0] as Item;

//            if (sender== RequiredItem)
//            {
//                CurrentAmount++;
//                Progess = QuestProgess.Active;
//                QuestStarted.Raise(this);
//            }

//            if (CurrentAmount >= RequiredAmount)
//            {
//                Progess = QuestProgess.Complete;
//                QuestEnded.Raise(this, RequiredItem);
//            }
//            QuestChange.Raise(this, RequiredItem);
//        }
//    }
//}
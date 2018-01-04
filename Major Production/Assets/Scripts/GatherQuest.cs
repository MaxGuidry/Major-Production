using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjects;

public enum Objective
{
    None = 0,
    Wood = 1,
    Stone = 2
}
public class GatherQuest : MonoBehaviour
{
    public Objective Objective;
    public Quest gatherQuest;
    public int GatherAmount;

    public List<Quest> ActiveQuests;
    private GameObject player;

    void Start()
    {
        player = gameObject;
        ActiveQuests = new List<Quest>();
    }

    void Update()
    {

        if(Objective == Objective.Wood)
            AddQuest(gatherQuest, "GatherLog", "Gather Log", GatherAmount);
        else if (Objective == Objective.Stone)
            AddQuest(gatherQuest, "GatherStone", "Gather Stone", GatherAmount);
    }

    public void AddQuest(Quest quest, string title, string objective, int requiredAmount)
    {
        if (quest == null) return;
        if (!ActiveQuests.Contains(quest))
            ActiveQuests.Add(quest);
        quest.Title = title;
        quest.QuestObjective = objective;
        quest.Progess = QuestProgess.Active;
        quest.RequiredAmount = requiredAmount;
        if(Objective == Objective.Wood)
            quest.CurrentAmount = player.GetComponent<InventoryBehaviour>().Wood.Count;
        else if (Objective == Objective.Stone)
            quest.CurrentAmount = player.GetComponent<InventoryBehaviour>().Stones.Count;
        if (quest.CurrentAmount == requiredAmount)
        {
            Debug.Log(quest.Title + " Complete");
            quest.Progess = QuestProgess.Complete;
            ActiveQuests.Remove(quest);
            gatherQuest = null;
        }
    }
}
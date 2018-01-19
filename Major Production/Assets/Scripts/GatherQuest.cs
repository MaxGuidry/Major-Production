using System;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

public enum Objective
{
    None = 0,
    Wood = 1,
    Stone = 2
}

public class GatherQuest : MonoBehaviour
{
    [Header("Active Quest")] public List<Quest> ActiveQuests;

    [Header("Gather Amount")] public int GatherAmount;

    [Header("Gather Quest")] public Quest gatherQuest;

    [Header("Gather Item")] public Objective objective;

    private GameObject _player;
    public Text QuestText;

    private void Start()
    {
        objective = Objective.None;
        _player = gameObject;
        ActiveQuests = new List<Quest>();
    }

    private void Update()
    {
        if (objective == Objective.None)
        {
            QuestText.text = "No Active Quest";
            QuestText.resizeTextForBestFit = true;
        }

        if (objective == Objective.Wood)
        {
            AddQuest(gatherQuest, "GatherLog", "Gather Log", GatherAmount);
            if (gatherQuest != null)
            {
                QuestText.text = "Current Quest:" + gatherQuest.Description + " " + gatherQuest.CurrentAmount + "/" +
                                 gatherQuest.RequiredAmount;
                QuestText.resizeTextForBestFit = true;
            }
            else
            {
                objective = Objective.None;
            }
        }
        else if (objective == Objective.Stone)
        {
            AddQuest(gatherQuest, "GatherStone", "Gather Stone", GatherAmount);
            if (gatherQuest != null)
            {
                QuestText.text = "Current Quest:" + gatherQuest.Description + " " + gatherQuest.CurrentAmount + "/" +
                                 gatherQuest.RequiredAmount;
                QuestText.resizeTextForBestFit = true;
            }
            else
            {
                objective = Objective.None;
            }
        }
    }

    public void AddQuest(Quest quest, string title, string description, int requiredAmount)
    {
        if (quest == null) return;
        if (!ActiveQuests.Contains(quest))
            ActiveQuests.Add(quest);
        quest.Title = title;
        quest.Description = description;
        quest.Progess = QuestProgess.Active;
        quest.RequiredAmount = requiredAmount;

        switch (this.objective)
        {
            case Objective.Wood:
                quest.CurrentAmount = _player.GetComponent<InventoryBehaviour>().Wood.Count;
                break;
            case Objective.Stone:
                quest.CurrentAmount = _player.GetComponent<InventoryBehaviour>().Stones.Count;
                break;
            case Objective.None:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        if (quest.CurrentAmount == requiredAmount)
        {
            Debug.Log(quest.Title + " Complete");
            quest.Progess = QuestProgess.Complete;
            ActiveQuests.Remove(quest);
            gatherQuest = null;
        }
    }
}
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
    private GameObject _playerStat;
    [Header("Active Quest")] public List<Quest> ActiveQuests;

    [Header("Gather Amount")] public int GatherAmount;

    [Header("Gather Quest")] public Quest gatherQuest;

    [Header("Gather Item")] public Objective objective;

    private GameObject _player;
    public Text QuestText;

    private void Start()
    {
        _playerStat = GameObject.FindGameObjectWithTag("Player");
        _player = gameObject;
        ActiveQuests = new List<Quest>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
            objective = Objective.None;
        if (Input.GetKeyDown(KeyCode.Keypad2))
            objective = Objective.Wood;
        if (Input.GetKeyDown(KeyCode.Keypad3))
            objective = Objective.Stone;

        if (objective == Objective.None)
        {
            QuestText.text = "No Active Quest";
            QuestText.resizeTextForBestFit = true;
        }
        if (objective == Objective.Wood)
        {
            AddQuest(gatherQuest, "GatherLog", "Gather Log", GatherAmount, 10);
        }
        else if (objective == Objective.Stone)
        {
            AddQuest(gatherQuest, "GatherStone", "Gather Stone", GatherAmount, 10);
        }
    }

    public void AddQuest(Quest quest, string title, string description, int requiredAmount, int exp)
    {
        if (quest == null) return;
        if (!ActiveQuests.Contains(quest))
            ActiveQuests.Add(quest);
        quest.Title = title;
        quest.Description = description;
        quest.Progess = QuestProgess.Active;
        quest.RequiredAmount = requiredAmount;
        QuestText.text = "Current Quest:" + gatherQuest.Description + " <color=green>" + gatherQuest.CurrentAmount + "/" +
                         gatherQuest.RequiredAmount + "</color>";
        QuestText.resizeTextForBestFit = true;
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
            _playerStat.GetComponent<PlayerStat>().ExpStat.AddModifier(exp);
            _playerStat.GetComponent<PlayerStat>().ArmorStat.AddModifier(5);
            _playerStat.GetComponent<PlayerStat>().ExpStat.GetValue();
            objective = Objective.None;
            return;
        }
    }
}
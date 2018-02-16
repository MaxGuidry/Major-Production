using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using ScriptableObjects;

[CanEditMultipleObjects]
public class ObjectiveEditor : EditorWindow {
    public string Title;
    [Multiline]
    public string Description;
    public ObjectiveType MissonType;
    public Item RequiredItem;
    public int CurrentAmount;
    public int RequiredAmount;
    public ObjectiveStatus Status;
    public GameObject Target;
    public Objective NextObjective;
    public List<ActionOnReach> ActionsOnReach;
    public Stat Mod;
    [MenuItem("CreateObjective/Start")]
    public static void ShowWindow()
    {
        GetWindow<ObjectiveEditor>("Start");
    }
    private void OnGUI()
    {
        Title = EditorGUILayout.TextField("Title", Title);
        Description = EditorGUILayout.TextField("Desiption", Description);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "GameKeys", menuName = "Game Keys", order = 0)]
public class GameKeys : ScriptableObject
{
    public List<string> optionsList;
    public List<KeyCode> keysList;

    [CustomEditor(typeof(GameKeys))]
    public class GameKeysEditor : Editor
    {
        private Event e;
        private bool listen = false;
        private List<string> options = new List<string>();
        private int index = 0;
        List<string> keys = new List<string>();
        List<KeyCode> tracker = new List<KeyCode>();
        public override void OnInspectorGUI()
        {
            e = Event.current;
            GameKeys g = target as GameKeys;
            options = g.optionsList;
            int i = 0;
            if(options == null)
                return;
            if (g.optionsList == null)
                return;
            foreach (var option in g.optionsList)
            {
                while (tracker.Count < g.keysList.Count)
                {
                    tracker.Add(KeyCode.None);
                }
                
                while (g.keysList.Count < g.optionsList.Count)
                {
                    g.keysList.Add(KeyCode.None);
                }
                while (keys.Count < g.optionsList.Count)
                {
                    keys.Add("");
                }
                if (g.keysList.Count > g.optionsList.Count)
                {
                    g.keysList.RemoveRange(g.optionsList.Count - 1, g.keysList.Count - g.optionsList.Count);
                }
                EditorGUILayout.LabelField(g.optionsList[i] + ": " + g.keysList[i]);

                keys[i] = GUILayout.TextField(keys[i]).ToLower();
                //Debug.Log(keys[i]);
                if (tracker[i] != g.keysList[i])
                {
                    continue;
                }
                if (keys[i] != "")
                {
                    g.keysList[i] = InputMap.WhatKeyCode(keys[i]);
                   // tracker[i] = g.keysList[i];
                }

                i++;
            }
            tracker = g.keysList;
            if (listen)
            {
                KeyCode code = KeyCode.None;

                if (e == null)
                    return;

                if (e.isKey)
                {
                    string str = e.character.ToString();
                    code = InputMap.WhatKeyCode(str);


                }
                if (e.isMouse)
                {
                    if (e.type == EventType.MouseDown)
                    {
                        code = InputMap.WhatMouseButton(e.button);

                    }

                }

            }
            base.OnInspectorGUI();
        }
    }

    private Dictionary<string, Action> actions;
    //public enum keys
    //{
    //    A,
    //    B,
    //    C,
    //    D,
    //    E,
    //    F,
    //    G,
    //    H,
    //    I,
    //    J,
    //    K,
    //    L,
    //    M,
    //    N,
    //    O,
    //    P,
    //    Q,
    //    R,
    //    S,
    //    T,
    //    U,
    //    V,
    //    W,
    //    X,
    //    Y,
    //    Z,
    //    LEFTCLICK,
    //    MIDDLECLICK,
    //    RIGHTCLICK,
    //    F1,
    //    F2,
    //    F3,
    //    F4,
    //    F5,
    //    F6,
    //    F7,
    //    F8,
    //    F9,
    //    F10,
    //    F11,
    //    F12,
    //    NUM1,
    //    NUM2,
    //    NUM3,
    //    NUM4,
    //    NUM5,
    //    NUM6,
    //    NUM7,
    //    NUM8,
    //    NUM9,
    //    NUM0

    //}
    
}

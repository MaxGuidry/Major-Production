using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class KeyMapButtons : MonoBehaviour
{
    Event e;
    // Use this for initialization
    public Canvas menu;
    public GameObject ButtonPrefab;

    //private string pressed;
    [SerializeField] GameKeys KeyOptions;
    private List<GameObject> buttons;
    void Start()
    {
        buttons = new List<GameObject>();
        List<string> names = new List<string>(KeyOptions.optionsList);
        List<KeyCode> codes = new List<KeyCode>(KeyOptions.keysList);
        int i = 0;

        if (InputMap.KeyBinds.Count == 0)
        {
            foreach (var c in codes)
            {
                InputMap.KeyBinds.Add(names[i], c);
                i++;
            }
        }

        //InputMap.Attack = KeyCode.Mouse0;
        //InputMap.Jump = KeyCode.Space;
        //InputMap.Interact = KeyCode.F;
        //InputMap.MoveForward = KeyCode.W;
        //InputMap.MoveLeft = KeyCode.A;
        //InputMap.MoveBack = KeyCode.S;
        //InputMap.MoveRight = KeyCode.D;
        int j = 0;
        int k = 5;
        foreach (var keyCode in KeyOptions.keysList)
        {
            GameObject g = Instantiate(ButtonPrefab);
            g.transform.SetParent(menu.gameObject.transform);
            g.transform.position = g.transform.parent.position;
            RectTransform rt = g.GetComponent<RectTransform>();
            rt.position =
                new Vector3(370, 208 + k * rt.sizeDelta.y, 0);
            // object o = settingsprop.GetValue(settingsprops, null);
            // string s = o.ToString();
            g.gameObject.GetComponentInChildren<Text>().text = KeyOptions.optionsList[j] + ": " + keyCode;
            g.gameObject.AddComponent<DisableWhenClicked>();
            g.gameObject.GetComponent<Button>().onClick.AddListener(StartWait);
            g.gameObject.GetComponent<Button>().onClick.AddListener(g.GetComponent<DisableWhenClicked>().disable);
            buttons.Add(g);
            j++;
            k--;
        }

        //PropertyInfo[] settingsprops = typeof(InputMap).GetProperties();

        //int i = 5;
        //foreach (var settingsprop in settingsprops)
        //{
        //    if (settingsprop.PropertyType == typeof(KeyCode))
        //    {
        //        GameObject g = Instantiate(ButtonPrefab);
        //        g.transform.SetParent(menu.gameObject.transform);
        //        g.transform.position = g.transform.parent.position;
        //        RectTransform rt = g.GetComponent<RectTransform>();
        //        rt.position =
        //            new Vector3(370, 208 + i * rt.sizeDelta.y, 0);
        //        object o = settingsprop.GetValue(settingsprops, null);
        //        string s = o.ToString();
        //        g.gameObject.GetComponentInChildren<Text>().text = settingsprop.Name + ": " + s;
        //        g.gameObject.AddComponent<DisableWhenClicked>();
        //        g.gameObject.GetComponent<Button>().onClick.AddListener(StartWait);
        //        g.gameObject.GetComponent<Button>().onClick.AddListener(g.GetComponent<DisableWhenClicked>().disable);
        //        buttons.Add(g);
        //        i--;
        //    }
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            InputMap.SaveSettings();


    }

    void OnDisable()
    {
        InputMap.SaveSettings();
    }

    void OnEnable()
    {
        InputMap.LoadSettings();
    }
    private void OnGUI()
    {
        e = Event.current;
    }



    public void StartWait()
    {

        StartCoroutine(waitforKeypress());

    }

    IEnumerator waitforKeypress()
    {
        bool key = false;
        KeyCode code = KeyCode.None;
        while (!key)
        {
            if (e == null)
                yield return null;
            if (e == null)
                continue;
            if (e.isKey)
            {
                key = true;
                string str = e.character.ToString();
                code = InputMap.WhatKeyCode(str);
                if (code == KeyCode.None)
                    code = InputMap.SpecialKey();
                break;
            }
            if (e.isMouse)
            {
                if (e.type == EventType.MouseDown)
                {
                    key = true;
                    code = InputMap.WhatMouseButton(e.button);
                    break;
                }
            }
            yield return null;
        }
        //PropertyInfo[] settingsprops = typeof(InputMap).GetProperties();
        int i = 0;
        foreach (var button in buttons)
        {


            if (!button.activeInHierarchy)
            {
                button.SetActive(true);
                //settingsprop.SetValue(this, code, null);
                List<KeyCode> keys = new List<KeyCode>();
                foreach (var keyBindsValue in InputMap.KeyBinds.Values)
                {
                    keys.Add(keyBindsValue);
                }
                keys[i] = code;
                List<string> names = new List<string>();
                foreach (var keyBindsKey in InputMap.KeyBinds.Keys)
                {
                    names.Add(keyBindsKey);
                }
                //object o = settingsprop.GetValue(settingsprops, null);
                string s = keys[i].ToString();
                buttons[i].GetComponentInChildren<Text>().text = names[i] + ": " + s;
                InputMap.KeyBinds.Clear();
                i = 0;
                foreach (var keyCode in keys)
                {
                    InputMap.KeyBinds.Add(names[i],keyCode);
                    i++;
                }
            }
            i++;

        }
    }


}





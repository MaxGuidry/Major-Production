using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupIfNotHardSet : MonoBehaviour {
    public List<SliderBehavior> PlayerOneSettings = new List<SliderBehavior>();
    public List<SliderBehavior> PlayerTwoSettings = new List<SliderBehavior>();
    public List<SliderBehavior> PlayerThreeSettings = new List<SliderBehavior>();
    public List<SliderBehavior> PlayerFourSettings = new List<SliderBehavior>();


    public List<CharacterMovement> characters = new List<CharacterMovement>();
    
    // Use this for initialization
    void Start () {
        SetupAllSettings();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetupAllSettings()
    {
        for (int i = 0; i < PlayerOneSettings.Count; i++)
        {
            PlayerOneSettings[i].objectToChange = characters[0];
        }

        if (characters.Count < 2)
            return;

        for (int i = 0; i < PlayerTwoSettings.Count; i++)
        {
            PlayerTwoSettings[i].objectToChange = characters[1];
        }
        if (characters.Count < 3)
            return;

        for (int i = 0; i < PlayerThreeSettings.Count; i++)
        {
            PlayerThreeSettings[i].objectToChange = characters[2];
        }
        if (characters.Count < 4)
            return;

        for (int i = 0; i < PlayerFourSettings.Count; i++)
        {
            PlayerFourSettings[i].objectToChange = characters[3];
        }
    }
}

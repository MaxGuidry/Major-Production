using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionsCooldown : MonoBehaviour
{
    private CharacterMovement character;
	// Use this for initialization
	void Start ()
	{
	    character = gameObject.transform.parent.parent.GetChild(0).GetComponent<CharacterMovement>();
	}
	
	// Update is called once per frame
	void Update ()
	{
        SetRadialSliders(character.MaxRocketCooldown, character.rocketCooldown, 0);
	    SetRadialSliders(character.MaxDashCooldown, character.dashCooldown, 1);
	    SetRadialSliders(character.MaxShieldCooldown, character.shieldCooldown, 2);
	    SetRadialSliders(character.MaxWhirlwindCooldown, character.whirlwindCooldown, 3);
    }

    private void SetRadialSliders(float maxCooldown, float coolDown, int child)
    {
        var slope = 1 / maxCooldown;
        var sliderAmount = slope * coolDown;
        gameObject.transform.GetChild(child).GetChild(1).GetComponent<Image>().fillAmount = sliderAmount;
    }
}

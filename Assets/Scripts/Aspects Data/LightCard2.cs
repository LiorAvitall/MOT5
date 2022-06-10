using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCard2 : CardData
{
	public LightCard2()
	{
		PrimodialPower = PowerType.Light;
	}

	public void Action()
	{
		Debug.Log($"Aspect Action: Light");
		// logic here
	}

	public void SupremeAction()
	{
		Debug.Log("Do Light Supreme Action");
	}

	public void SecondaryAction()
	{
		Debug.Log("Do Light Secondary Action");
	}
}

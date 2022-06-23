using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Light Card", menuName = "Old Card/Light")]
public class OldLightCard : OldCardData
{
	public OldLightCard()
	{
		PrimodialPower = PowerType.Light;
	}

	public void Action(OldEventHandler eventHandler)
	{
		eventHandler.DrawCard();
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

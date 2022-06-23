using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Life Card", menuName = "Card/Life")]
public class OldLifeCard : OldCardData
{
	public OldLifeCard()
	{
		PrimodialPower = PowerType.Life;
	}

	public void Action(OldEventHandler eventHandler)
	{
		eventHandler.Revive();
	}

	public void SupremeAction()
	{
		Debug.Log("Do Life Supreme Action");
	}

	public void SecondaryAction()
	{
		Debug.Log("Do Life Secondary Action");
	}
}

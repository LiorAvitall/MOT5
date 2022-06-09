using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Life Card", menuName = "Card/Life")]
public class LifeCard : CardData
{
	public LifeCard()
	{
		PrimodialPower = PowerType.Life;
	}

	public void Action(EventHandler eventHandler)
	{
		// fix needed
		eventHandler.DrawCard();
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

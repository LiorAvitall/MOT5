using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Death Card", menuName = "Card/Death")]
public class DeathCard : CardData {

	public DeathCard()
	{
		PrimodialPower = PowerType.Death;
	}

	public void Action(EventHandler eventHandler)
	{
		// fix needed
		eventHandler.Sacrifice();
	}

	public void SupremeAction()
	{
		Debug.Log("Do Death Supreme Action");
	}

	public void SecondaryAction()
	{
		Debug.Log("Do Death Secondary Action");
	}
}

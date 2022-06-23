using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Death Card", menuName = "Old Card/Death")]
public class OldDeathCard : OldCardData {

	public OldDeathCard()
	{
		PrimodialPower = PowerType.Death;
	}

	public void Action(OldEventHandler eventHandler)
	{
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

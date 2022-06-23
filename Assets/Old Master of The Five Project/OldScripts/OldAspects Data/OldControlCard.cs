using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Control Card", menuName = "Card/Control")]
public class OldControlCard : OldCardData
{
	public OldControlCard()
    {
		PrimodialPower = PowerType.Control;
	}

	public void Action(OldEventHandler eventHandler)
	{
		// fix needed
		eventHandler.DrawCard();
	}

	public void SupremeAction()
	{
		Debug.Log("Do Control Supreme Action");
	}

	public void SecondaryAction()
	{
		Debug.Log("Do Control Secondary Action");
	}
}

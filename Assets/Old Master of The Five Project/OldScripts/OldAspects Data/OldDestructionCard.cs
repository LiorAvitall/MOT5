using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Destruction Card", menuName = "Old Card/Destruction")]
public class OldDestructionCard : OldCardData
{
	public OldDestructionCard()
	{
		PrimodialPower = PowerType.Destruction;
	}

	public void Action(OldEventHandler eventHandler)
	{
		// fix needed
		eventHandler.Destroy();
	}

	public void SupremeAction()
	{
		Debug.Log("Do Destruction Supreme Action");
	}

	public void SecondaryAction()
	{
		Debug.Log("Do Destruction Secondary Action");
	}
}

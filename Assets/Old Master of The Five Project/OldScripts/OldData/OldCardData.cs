using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OldPowerType {Light, Death, Destruction, Life, Control};

public class OldCardData : ScriptableObject
{
	public string Name, Description;
	public Sprite Artwork, CardTemplate;
	public PowerType PrimodialPower;

	public void Print()
	{
		Debug.Log($"Name: {Name}, Power Type: {PrimodialPower}, Description: {Description}");
	}
}

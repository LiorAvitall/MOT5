using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerType {Light, Death, Destruction, Life, Control};

public class CardData : ScriptableObject
{
	public string Name, Description;
	public Sprite Artwork, CardTemplate;
	public int ManaCost, Attack, Health;
	public PowerType PrimodialPower;

	public void Print()
	{
		Debug.Log($"Name: {Name}, Power Type: {PrimodialPower}, Description: {Description}, Mana cost: {ManaCost}");
	}
}

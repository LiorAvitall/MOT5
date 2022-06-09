using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class CardDisplay : MonoBehaviour
{
	public CardData CardData;
	public Image ArtworkImage, CardTemplate;
	public TextMeshProUGUI NameText, DescriptionText;
	public TextMeshProUGUI ManaText, AttackText, HealthText;

	// Use this for initialization
	void Start()
	{
		ArtworkImage.sprite = CardData.Artwork;

		NameText.text = CardData.Name;
		DescriptionText.text = CardData.Description;
		
		ManaText.text = CardData.ManaCost.ToString();
		AttackText.text = CardData.Attack.ToString();
		HealthText.text = CardData.Health.ToString();

        switch (CardData.PrimodialPower)
        {
            case PowerType.Light:
                CardTemplate.color = Color.yellow;
                break;

            case PowerType.Death:
                CardTemplate.color = Color.black;
                break;

            case PowerType.Destruction:
                CardTemplate.color = Color.red;
                break;

            case PowerType.Life:
                CardTemplate.color = Color.green;
                break;

            case PowerType.Control:
                CardTemplate.color = Color.blue;
                break;

            default:
                CardTemplate.color = Color.white;
                break;
        }
	}
}

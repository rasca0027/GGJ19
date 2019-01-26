using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player 
{
	public string name;
	public string occupation;
	public string nativeLanguage;
	
	// stats
	public float technicalSkills;
	public float stressHappyLevel;
	public float languageAbility;
	public float networkCircle;

	public Player(string playerName)
	{
		name = playerName;
		occupation = "Game Dev";
		nativeLanguage = "Mandarin";
		technicalSkills = 0.5f;
		stressHappyLevel = 0.5f;
		languageAbility = 0.5f;
		networkCircle = 0.5f;
	}


	public void ImproveTechnicalSkills()
	{

	}

	public void TalkToFamily()
	{

	}

	public void ExpandNetwork()
	{

	}

}

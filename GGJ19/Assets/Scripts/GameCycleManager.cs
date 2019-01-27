using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCycleManager : MonoBehaviour {

	public Slider jobSlider;
	public Slider stressSlider;
	public Slider languageSlider;
	public Slider friendSlider;
	public Text remainingActions;
	public Text weekText;


	int weekNum; // 1 ~ 52
	int actions;
	int maxActions;
	Player player;
	GameState state;

    public enum GameState 
	{
		Weekday,
		Weekend,
		End
	}

	// Use this for initialization
	void Start () {
		player = new Player("kelly");
		weekNum = 1;
		maxActions = 12;
		actions = maxActions;
		state = GameState.Weekday;
	}
	
	// Update is called once per frame
	void Update () {

		if (state == GameState.Weekday)
		{
			if (actions == 0)
			{
				state = GameState.Weekend;
			}
		}
		else if (state == GameState.Weekend)
		{
			weekNum += 1;
			actions = maxActions;
		}
		else // End, evaluate visa
		{

		}	

		// change slider value
		jobSlider.value = player.technicalSkills;
		stressSlider.value = player.stressHappyLevel;
		languageSlider.value = player.languageAbility;
		friendSlider.value = player.networkCircle;
		
		remainingActions.text = actions.ToString();
		weekText.text = "week " + weekNum.ToString();

	}

	public void buttonClicked()
	{
		actions -= 1;
	}

	public void work()
	{

	}

	public void TalkToFamily()
	{

	}

	public void ExpandNetwork()
	{
		Debug.Log("dress");
	}

}

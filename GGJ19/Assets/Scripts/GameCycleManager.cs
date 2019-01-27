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
	public GameObject infoText;


	int weekNum; // 1 ~ 52
	int actions;
	int maxActions;
	Player player;
	GameState state;
	bool visaStatus;

	List<string> actionsThisRound;

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
		maxActions = 3;
		actions = maxActions;
		state = GameState.Weekday;
		visaStatus = false;
		actionsThisRound = new List<string>(); 
	}
	
	// Update is called once per frame
	void Update () {

		if (state == GameState.Weekday)
		{

			if (weekNum >= 53)
			{
				state = GameState.End;
			}
		}
		else if (state == GameState.Weekend)
		{
			actions = maxActions;
			state = GameState.Weekday;
		}
		else // End, evaluate visa
		{
			if (player.technicalSkills >= 0.75f && player.stressHappyLevel >= 0.35f && player.languageAbility >= 0.45f)
			{

				visaStatus = true;
			} 
		}	

		// change slider value
		jobSlider.value = player.technicalSkills;
		stressSlider.value = player.stressHappyLevel;
		languageSlider.value = player.languageAbility;
		friendSlider.value = player.networkCircle;
		
		remainingActions.text = actions.ToString();
		weekText.text = "week " + weekNum.ToString();

	}

	private void buttonClicked(string res)
	{
		actions -= 1;
		StartCoroutine(ShowInfo(res));
	}

	IEnumerator ShowInfo(string res)
    {
		infoText.SetActive(true);
		infoText.transform.GetChild(0).GetComponent<Text>().text = res;
        yield return new WaitForSeconds(2);
		infoText.SetActive(false);
		if (actions == 0)
		{
			state = GameState.Weekend;
			weekNum += 2;
			StartCoroutine(ShowInfo("Week " + weekNum.ToString()));
		}
    }

	public void Read()
	{
		Debug.Log("read");
		buttonClicked("You read a sad romance story and cried all night. Happiness dropped.");
		actionsThisRound.Add("Work");
		player.stressHappyLevel -= 0.1f;
		if (player.stressHappyLevel< 0f)
		{
			player.stressHappyLevel = 0f;
		}

	}
	public void ImproveSkills()
	{

		Debug.Log("work");
		buttonClicked("You worked hard and gain technical skills, but you became more stressed.");
		actionsThisRound.Add("Work");
		player.stressHappyLevel -= 0.2f;
		if (player.stressHappyLevel< 0f)
		{
			player.stressHappyLevel = 0f;
		}
		player.technicalSkills += 0.2f;
		if (player.technicalSkills > 1f)
		{
			player.technicalSkills = 1f;
		}

	}

	public void WorkOvertime()
	{

		Debug.Log("work");
		buttonClicked("You stayed up all night to finish this new feature. Your boss is happy but your family discovered you were online all night and they are mad now.");
		actionsThisRound.Add("Work");
		player.networkCircle -= 0.05f;
		if (player.stressHappyLevel< 0f)
		{
			player.stressHappyLevel = 0f;
		}
		player.technicalSkills += 0.1f;
		if (player.technicalSkills > 1f)
		{
			player.technicalSkills = 1f;
		}

	}

	public void GotoMeetups()
	{

		Debug.Log("meetup");
		buttonClicked("You went to a technical meetup and met a lot of new people. But you got discriminated because you are a girl.");
		actionsThisRound.Add("meetup");
		player.stressHappyLevel -= 0.3f;
		if (player.stressHappyLevel < 0f)
		{
			player.stressHappyLevel = 0f;
		}
		player.networkCircle -= 0.2f;
		if (player.technicalSkills < 0f)
		{
			player.technicalSkills = 0f;
		}

	}
	public void CatchUpWithHomeFriends()
	{

		Debug.Log("home friends");
		buttonClicked("You worked hard and gain technical skills, but you became more stressed.");
		actionsThisRound.Add("HomeFriends");
		player.languageAbility -= 0.2f;
		if (player.languageAbility < 0f)
		{
			player.stressHappyLevel = 0f;
		}
		player.networkCircle += 0.2f;
		if (player.networkCircle > 1f)
		{
			player.networkCircle = 1f;
		}

		player.stressHappyLevel += 0.1f;
		if (player.stressHappyLevel > 1f)
			player.stressHappyLevel = 1f;

	}
	public void TalkToFamily()
	{
		Debug.Log("family");
		buttonClicked("You called home. Your parents are happy to hear your voice. You talked for 2 hours and forgot how to speak English.");
		actionsThisRound.Add("Family");
		player.languageAbility -= 0.2f;
		if (player.languageAbility < 0f)
		{
			player.languageAbility = 0f;
		}
		player.stressHappyLevel += 0.1f;
		if (player.stressHappyLevel > 1f)
		{
			player.stressHappyLevel = 1f;
		}
	}

	public void Hiking()
	{
		Debug.Log("hike");
		actionsThisRound.Add("Hike");
		buttonClicked("You went for a hike.");

	}
	public void ExpandNetwork()
	{
		Debug.Log("dress");
		actionsThisRound.Add("ExpandNetwork");
		buttonClicked("You hangout with friends and partied all night. You were late for work and now your boss is unhappy.");
		player.networkCircle -= 0.1f;
		if (player.networkCircle < 0f)
		{
			player.networkCircle = 0f;
		}
		player.technicalSkills -= 0.1f;
		if (player.technicalSkills < 0f)
			player.technicalSkills = 0f;
	}

	public void PlayGames()
	{
		Debug.Log("games");
		actionsThisRound.Add("PlayGame");
		buttonClicked("You beat Red Dead Redemption in 1 day and now you feel guilty for spending some much time on it.");
		player.stressHappyLevel -= 0.1f;
		if (player.stressHappyLevel < 0f)
		{
			player.stressHappyLevel = 0f;
		}
	}
	
	public void PlayMusic()
	{
		Debug.Log("music");
		buttonClicked("You listened to your favorite black metal band Alcest and practiced some of the songs. Your fingers are tired and you forgot how to code.");
		actionsThisRound.Add("PlayMusic");
		player.technicalSkills -= 0.1f;
		if (player.technicalSkills < 0f)
		{
			player.technicalSkills = 0f;
		}
		player.stressHappyLevel += 0.15f;
		if (player.stressHappyLevel > 1f)
		{
			player.stressHappyLevel = 1f;
		}
	}
}

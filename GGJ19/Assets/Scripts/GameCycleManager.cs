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
	public GameObject title;
	public GameObject news;
	public GameObject room;

	public Sprite normal;
	public Sprite happy;
	public Sprite sad;
	public Sprite dying;
	public Sprite dead;
	public Image portrait;
	public GameObject X;


	int weekNum; // 1 ~ 52
	int actions;
	int maxActions;
	Player player;
	GameState state;
	bool visaStatus;
	int mood;

	List<string> actionsThisRound;

    public enum GameState 
	{
		Weekday,
		Weekend,
		End,
		Ending
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

		switch(mood)
		{
			case 0:
				portrait.sprite = normal;
				break;
			case 1:
				portrait.sprite = happy;
				break;
			case 2:
				portrait.sprite = sad;
				break;
			case 3:
				portrait.sprite = dying;
				break;
			case 4:
				portrait.sprite = dead;
				break;
		}
		if (player.stressHappyLevel < 0.4)
			mood = 2;
		if (player.stressHappyLevel < 0.2)
			mood = 3;
		if (player.stressHappyLevel < 0.1)
			mood = 4;


		if (state == GameState.Weekday)
		{
			if (weekNum == 7) {
				ShowNews("Alon Musk like billionaire decides to tackle immigration. Your chance of getting visa is lower now.");
				weekNum += 2;
				state = GameState.Weekend;
				StartCoroutine(ShowInfo("Week " + weekNum.ToString()));
			}
			if (weekNum == 11) {
				ShowNews("Your immigration lawyer dies of snorting sugar.");
				weekNum += 2;
				state = GameState.Weekend;
				StartCoroutine(ShowInfo("Week " + weekNum.ToString()));
			}

			if (weekNum == 17) {
				ShowNews("Ronald Drump decides golf skills be made a criteria for the GR-8 visa.");
				weekNum += 2;
				state = GameState.Weekend;
				StartCoroutine(ShowInfo("Week " + weekNum.ToString()));
			}

			if (weekNum == 23) {
				ShowNews("The immigration website gets hacked by FSociety.");
				weekNum += 2;
				state = GameState.Weekend;
				StartCoroutine(ShowInfo("Week " + weekNum.ToString()));
			}
			if (weekNum >= 27)
			{
				state = GameState.End;
			}
		}
		else if (state == GameState.Weekend)
		{
			actions = maxActions;
			state = GameState.Weekday;
		}
		else if (state == GameState.End) // End, evaluate visa
		{
			if (player.technicalSkills >= 0.75f && player.stressHappyLevel >= 0.35f && player.languageAbility >= 0.45f)
			{
				ShowNews("It's time to apply for VISA. Your working skill and English skill is good enough to get the VISA. Congrats!");
			} 
			else
				ShowNews("Unfortunately, you didn't get the VISA. Try again (in your next life).");
			X.SetActive(false);
		}	
		


		// change slider value
		jobSlider.value = player.technicalSkills;
		stressSlider.value = player.stressHappyLevel;
		languageSlider.value = player.languageAbility;
		friendSlider.value = player.networkCircle;
		
		remainingActions.text = actions.ToString();
		weekText.text = "week " + weekNum.ToString();

		if (Input.GetKey("escape"))
        {
            ReturnToTitle();
        }

	

	}

	private void ShowNews(string newsText)
	{
		room.SetActive(false);
		news.SetActive(true);

		news.transform.GetChild(0).GetComponent<Text>().text = newsText;
	}

	public void CloseNews()
	{
		news.SetActive(false);
		room.SetActive(true);
	}

	private void ReturnToTitle()
	{
		state = GameState.Weekday;
		weekNum = 1;
		title.SetActive(true);
	}

	public void StartGame()
	{
		title.SetActive(false);
	}

	public void EndGame()
	{
		#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
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
        yield return new WaitForSeconds(3);
		infoText.SetActive(false);
		if (actions <= 0)
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

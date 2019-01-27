using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomButton : MonoBehaviour {

	public GameObject hoverText;
	public GameObject gameManager;
	public string actionText = "";
	public string actionTextAlt = "";
	public int actionID;
	Vector3 oldTransform;
	GameCycleManager gcm;
	bool toggle = false;

	// Use this for initialization
	void Start()
	{
		gcm = gameManager.GetComponent<GameCycleManager>();

	}


	void OnMouseOver()
	{
		oldTransform = transform.localScale;
		transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
		hoverText.SetActive(true);
		hoverText.transform.GetChild(0).GetComponent<Text>().text = actionText;
	}

	void OnMouseUp()
	{

		switch (actionID)
		{
			case 1:
				if (toggle)
					gcm.ImproveSkills();
				else
					gcm.Read();
				ToggleText();
				break;
			case 2:
				if (toggle)
					gcm.TalkToFamily();
				else
					gcm.CatchUpWithHomeFriends();
				ToggleText();
				break;
			case 3:
				gcm.PlayMusic();
				break;
			case 4:
				if (toggle)
					gcm.ExpandNetwork();
				else
					gcm.GotoMeetups();
				ToggleText();
				break;
			case 5:
				if (toggle)
					gcm.PlayGames();
				else
					gcm.WorkOvertime();
				ToggleText();
				break;
			case 6:
				gcm.Hiking();
				break;

				
		}
	}

	void OnMouseExit()
	{
		//transform.localScale = oldTransform;
		transform.localScale = new Vector3(1f, 1f, 1f);
		hoverText.SetActive(false);
	}

	void ToggleText()
	{
		if (actionTextAlt != "")
		{
			string tmp = actionText;
			actionText = actionTextAlt;
			actionTextAlt = tmp;
			toggle = !toggle;
		}
	}
}

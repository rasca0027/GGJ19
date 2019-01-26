using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCycleManager : MonoBehaviour {

	int weekNum; // 1 ~ 52
	int actionPts;
	int maxActionPts;
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
		maxActionPts = 12;
		actionPts = maxActionPts;
		state = GameState.Weekday;
	}
	
	// Update is called once per frame
	void Update () {
		if (state == GameState.Weekday)
		{

		}
		else if (state == GameState.Weekend)
		{

		}
		else // End, evaluate visa
		{

		}	
	}
}

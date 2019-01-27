using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomButton : MonoBehaviour {

	public GameObject hoverText;
	public string actionText = "";
	Vector3 oldTransform;
	// Use this for initialization

	void OnMouseOver()
	{
		oldTransform = transform.localScale;
		transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
		hoverText.SetActive(true);
		hoverText.transform.GetChild(0).GetComponent<Text>().text = actionText;
	}

	void OnMouseExit()
	{
		//transform.localScale = oldTransform;
		transform.localScale = new Vector3(1f, 1f, 1f);
		hoverText.SetActive(false);
	}
}

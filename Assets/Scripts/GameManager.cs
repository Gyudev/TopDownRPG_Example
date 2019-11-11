using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	private Text talkText;
	private GameObject scanObject;

	private void Start()
	{
		talkText = GameObject.Find("Canvas").transform.Find("Image").GetComponentInChildren<Text>();
	}

	public void Action(GameObject gameObj)
	{
		scanObject = gameObj;
		talkText.text = scanObject.name + "입니다.";
	}
}

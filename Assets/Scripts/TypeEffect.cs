using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeEffect : MonoBehaviour
{
	private GameObject talkPanel;
	private GameObject endCursor;

	private int CharPerSeconds = 10;
	private string targetMsg;
	private Text msgText;
	private int index;

	private void Awake()
	{
		talkPanel = GameObject.Find("Canvas").transform.Find("Talk Set").GetComponent<GameObject>();
		endCursor = talkPanel.transform.Find("End Cursor").GetComponent<GameObject>();
		msgText = GetComponent<Text>();
	}

	public void SetMsg(string msg)
	{
		targetMsg = msg;
		EffectStart();
	}

	private void EffectStart()
	{
		msgText.text = "";
		index = 0;
		endCursor.SetActive(false);
		Invoke("Effecting", 1 / CharPerSeconds);
	}

	private void Effecting()
	{
		if(msgText.text == targetMsg)
		{
			EffectEnd();
			return;
		}
		msgText.text += targetMsg[index];
		index++;

		Invoke("Effecting", 1 / CharPerSeconds);
	}

	private void EffectEnd()
	{
		endCursor.SetActive(true);
	}
}

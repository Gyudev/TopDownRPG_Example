using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeEffect : MonoBehaviour
{
	private Transform talkPanel;
	private Transform endCursor;
	public bool isAnim;

	private Text msgText;
	private AudioSource audioSource;

	private int CharPerSeconds = 15;
	private string targetMsg;
	private int index;

	private float interval;

	private void Awake()
	{
		talkPanel = GameObject.Find("Canvas").transform.Find("Talk Set").GetComponent<Transform>();
		endCursor = talkPanel.transform.Find("End Cursor").GetComponent<Transform>();
		msgText = GetComponent<Text>();
		audioSource = GetComponent<AudioSource>();
	}

	public void SetMsg(string msg)
	{
		if (isAnim)
		{
			msgText.text = targetMsg;
			CancelInvoke();
			EffectEnd();
		}
		else
		{
			targetMsg = msg;
			EffectStart();
		}
	}

	private void EffectStart()
	{
		msgText.text = "";
		index = 0;
		endCursor.gameObject.SetActive(false);

		interval = 1.0f / CharPerSeconds;
		Debug.Log(interval);

		isAnim = true;
		Invoke("Effecting", interval);
	}

	private void Effecting()
	{
		if(msgText.text == targetMsg)
		{
			EffectEnd();
			return;
		}
		msgText.text += targetMsg[index];
		//사운드
		if(targetMsg[index] != ' ' || targetMsg[index] != '.')
		{
			audioSource.Play();
		}

		index++;

		Invoke("Effecting", interval);
	}

	private void EffectEnd()
	{
		isAnim = false;
		endCursor.gameObject.SetActive(true);
	}
}

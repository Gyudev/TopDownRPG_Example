using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	private TalkManager talkManager;

	private Text talkText;
	private Image portraitImg;
	private Transform talkPanel;
	private GameObject scanObject;

	public bool isAction;
	public int talkIndex;

	private void Start()
	{
		talkManager = GameObject.Find("TalkManager").GetComponent<TalkManager>();
		talkText = GameObject.Find("Canvas").transform.Find("Talk Set").GetComponentInChildren<Text>();
		talkPanel = GameObject.Find("Canvas").transform.Find("Talk Set").GetComponent<Transform>();
		portraitImg =talkPanel.transform.Find("Portrait").GetComponent<Image>();
	}

	public void Action(GameObject gameObj)
	{

		scanObject = gameObj;
		ObjData objData = scanObject.GetComponent<ObjData>();
		Talk(objData.id, objData.isNPC);


		talkPanel.gameObject.SetActive(isAction);
	}

	private void Talk(int id, bool isNPC)
	{
		string talkData = talkManager.GetTalk(id, talkIndex);

		if(talkData == null)
		{
			isAction = false;
			talkIndex = 0;
			return;
		}
		if (isNPC)
		{
			talkText.text = talkData.Split(':')[0];
			portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
			portraitImg.color = new Color(1, 1, 1, 1);
		}
		else
		{
			talkText.text = talkData;

			portraitImg.color = new Color(1, 1, 1, 0);
		}

		isAction = true;
		talkIndex++;
	}
}

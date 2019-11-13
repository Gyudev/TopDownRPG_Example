using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	private TalkManager talkManager;
	private QuestManager questManager;
	private TypeEffect typeEffect;

	//private Text talkText;
	private Image portraitImg;

	private Animator talkPanel;
	private Animator portraitAnim;
	private Sprite prevPortrait;

	private GameObject scanObject;

	public bool isAction;
	public int talkIndex;

	private void Start()
	{
		talkManager = GameObject.Find("TalkManager").GetComponent<TalkManager>();
		questManager = GameObject.Find("QuestManager").GetComponent<QuestManager>();
		
		//talkText = GameObject.Find("Canvas").transform.Find("Talk Set").GetComponentInChildren<Text>();
		talkPanel = GameObject.Find("Canvas").transform.Find("Talk Set").GetComponent<Animator>();
		typeEffect = talkPanel.transform.Find("Text").GetComponent<TypeEffect>();
		portraitImg = talkPanel.transform.Find("Portrait").GetComponent<Image>();
		portraitAnim = talkPanel.transform.Find("Portrait").GetComponent<Animator>();

		Debug.Log(questManager.CheckQuest());
	}

	public void Action(GameObject gameObj)
	{

		scanObject = gameObj;
		ObjData objData = scanObject.GetComponent<ObjData>();
		Talk(objData.id, objData.isNPC);


		talkPanel.SetBool("isShow", isAction);
	}

	private void Talk(int id, bool isNPC)
	{
		int questTalkIndex = 0;
		string talkData = "";

		if (typeEffect.isAnim)
		{
			typeEffect.SetMsg("");
			return;
		}
		else
		{
			questTalkIndex = questManager.GetQuestTalkIndex(id);
			talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);
		}

		if(talkData == null)
		{
			isAction = false;
			talkIndex = 0;
			Debug.Log(questManager.CheckQuest(id));
			return;
		}
		if (isNPC)
		{
			typeEffect.SetMsg(talkData.Split(':')[0]);
			portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
			portraitImg.color = new Color(1, 1, 1, 1);

			//초상화 이펙트
			if(prevPortrait != portraitImg.sprite)
			{
				portraitAnim.SetTrigger("doEffect");
				prevPortrait = portraitImg.sprite;
			}
		}
		else
		{
			typeEffect.SetMsg(talkData);
			portraitImg.color = new Color(1, 1, 1, 0);
		}

		isAction = true;
		talkIndex++;
	}
}

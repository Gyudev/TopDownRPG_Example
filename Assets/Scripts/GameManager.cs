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
	private Sprite prevPortrait;

	private Animator talkPanel;
	private Animator portraitAnim;

	private GameObject scanObject;
	private Transform player;

	private Transform menuSet;
	private Transform buttonPanel;

	private Text questText;

	private Button exitButton;
	private Button saveButton;
	
	public bool isAction;
	public int talkIndex;

	private void Start()
	{
		talkManager = GameObject.Find("TalkManager").GetComponent<TalkManager>();
		questManager = GameObject.Find("QuestManager").GetComponent<QuestManager>();
		player = GameObject.Find("Player").GetComponent<Transform>();

		//talkText = GameObject.Find("Canvas").transform.Find("Talk Set").GetComponentInChildren<Text>();
		talkPanel = GameObject.Find("Canvas").transform.Find("Talk Set").GetComponent<Animator>();
		typeEffect = talkPanel.transform.Find("Text").GetComponent<TypeEffect>();
		portraitImg = talkPanel.transform.Find("Portrait").GetComponent<Image>();
		portraitAnim = talkPanel.transform.Find("Portrait").GetComponent<Animator>();
		menuSet = GameObject.Find("Canvas").transform.Find("Menu Set").GetComponent<Transform>();
		questText =menuSet.transform.Find("Quest Panel").GetComponentInChildren<Text>();
		buttonPanel = menuSet.transform.Find("Button Panel").GetComponent<Transform>();

		exitButton = buttonPanel.transform.Find("Exit").GetComponent<Button>();
		exitButton.onClick.AddListener(GameExit);

		saveButton = buttonPanel.transform.Find("Save").GetComponent<Button>();
		saveButton.onClick.AddListener(GameSave);

		GameLoad();
		questText.text = questManager.CheckQuest();
	}

	private void Update()
	{
		//서브 메뉴
		if (Input.GetButtonDown("Cancel"))
		{
			if (menuSet.gameObject.activeSelf)
			{
				menuSet.gameObject.SetActive(false);
				Time.timeScale = 1;
			}
			else
			{
				Time.timeScale = 0;
				menuSet.gameObject.SetActive(true);
			}
		}
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
			questText.text = questManager.CheckQuest(id);
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

	private void GameSave()
	{
		//플레이어 x, y좌표
		//퀘스트 id, Action Index
		PlayerPrefs.SetFloat("PlayerX", player.position.x);
		PlayerPrefs.SetFloat("PlayerY", player.position.y);
		PlayerPrefs.SetInt("QuestId", questManager.questId);
		PlayerPrefs.SetInt("QuestActionIndex", questManager.questActionIndex);
		PlayerPrefs.Save();

		menuSet.gameObject.SetActive(false);
		Time.timeScale = 1;

	}

	private void GameLoad()
	{
		if (!PlayerPrefs.HasKey("PlayerX"))
		{
			return;
		}
		else
		{

			float x = PlayerPrefs.GetFloat("PlayerX");
			float y = PlayerPrefs.GetFloat("PlayerY");
			int questId = PlayerPrefs.GetInt("QuestId");
			int questActionIndex = PlayerPrefs.GetInt("QuestActionIndex");

			player.position = new Vector3(x, y, -1);
			questManager.questId = questId;
			questManager.questActionIndex = questActionIndex;
			questManager.ControlObject();
		}
	}

	private void GameExit()
	{
		Application.Quit();
	}
}

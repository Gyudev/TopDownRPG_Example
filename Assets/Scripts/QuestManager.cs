using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
	private int questId = 10;
	private int questActionIndex = 0;
	public GameObject[] questObject;

	private Dictionary<int, QuestData> questList;

	private void Awake()
	{
		questList = new Dictionary<int, QuestData>();
		GenerateData();
	}

	private void GenerateData()
	{
		questList.Add(10, new QuestData("마을 사람들과 대화하기.", new int[] { 1000, 2000 }));
		questList.Add(20, new QuestData("구린스의 동전 찾아주기.", new int[] { 100, 200 }));
	}

	public int GetQuestTalkIndex(int id)
	{
		return questId + questActionIndex;
	}

	public string CheckQuest(int id)
	{
		if (id == questList[questId].npcId[questActionIndex])
		{
			questActionIndex++;
		}

		ControlObject();

		if (questActionIndex == questList[questId].npcId.Length)
		{
			NextQuest();
		}

		return questList[questId].questName;
	}

	private void NextQuest()
	{
		questId += 10;
		questActionIndex = 0;
	}

	private void ControlObject()
	{
		switch (questId)
		{
			case 10:
				if(questActionIndex == 2)
				{
					questObject[0].SetActive(true);
				}
				break;
			case 20:
				if (questActionIndex == 1)
				{
					questObject[0].SetActive(false);
				}
				break;
		}
	}
}

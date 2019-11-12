using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
	private int questId = 10;
	public int questActionIndex;
	private Dictionary<int, QuestData> questList;

	private void Awake()
	{
		questList = new Dictionary<int, QuestData>();
		GenerateData();
	}

	private void GenerateData()
	{
		questList.Add(10, new QuestData("마을 사람들과 대화하기.", new int[] { 1000, 2000 }));
	}

	public int GetQuestTalkIndex(int id)
	{
		return questId + questActionIndex;
	}

	public void CheckQuest()
	{
		questActionIndex++;
	}
}

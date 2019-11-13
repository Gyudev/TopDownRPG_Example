using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
	Dictionary<int, string[]> talkData;
	Dictionary<int, Sprite> portraitData;

	public Sprite[] portraitArr;

	private void Awake()
	{
		talkData = new Dictionary<int, string[]>();
		portraitData = new Dictionary<int, Sprite>();
		GenerateData();
	}

	private void GenerateData()
	{
		//대화 데이터
		talkData.Add(1000, new string[] { "으윽 더러워..:3", "진짜 못 생겼구나?:2", "돈 있냐?:1" });
		talkData.Add(2000, new string[] { "....:1" });
		talkData.Add(100, new string[] { "3대 500을 치면 부술 수 있을 것 같다.", "안에 무엇이 있을까?" });
		talkData.Add(200, new string[] { "종혁이의 아픔이 있는 책상이다." });

		//퀘스트 데이터
		talkData.Add(10 + 1000, new string[] { "반가워:0","모험가가 되고싶은거야?:1", "남서쪽 나무 밑에 구린스가 알려줄거야.:2" });
		talkData.Add(11 + 2000, new string[] { "뭐야?:3", "모험가가 되고싶다고?:1", "그럼 어딘가 떨어진 내 동전좀 찾아와.:2" });

		talkData.Add(20 + 1000, new string[] { "구린스의 동전?:3", "어디 떨어뜨렸데?:1", "몰라? 한마디 해야겠네.:2" });
		talkData.Add(20 + 2000, new string[] { "얼른 찾아줘..:0"});

		talkData.Add(20 + 5000, new string[] { "구린스의 동전인거 같다." });

		talkData.Add(21 + 2000, new string[] { "고마워!:2" });


		portraitData.Add(1000 + 0, portraitArr[0]);
		portraitData.Add(1000 + 1, portraitArr[1]);
		portraitData.Add(1000 + 2, portraitArr[2]);
		portraitData.Add(1000 + 3, portraitArr[3]);
		portraitData.Add(2000 + 0, portraitArr[4]);
		portraitData.Add(2000 + 1, portraitArr[5]);
		portraitData.Add(2000 + 2, portraitArr[6]);
		portraitData.Add(2000 + 3, portraitArr[7]);
	}

	public string GetTalk(int id, int talkIndex)
	{
		//예외처리 (다듬은 로직)
		if (!talkData.ContainsKey(id))
		{
			if (!talkData.ContainsKey(id - id % 10))
			{
				//퀘스트의 처음 대사마저 없을 때
				//기본 대사를 가지고 옴.
				return GetTalk(id - id % 100, talkIndex);
			}
			else
			{
				//해당 퀘스트 진행 순서 대사가 없을 때
				//퀘스트 맨 처음 대사를 가지고 옴.
				return GetTalk(id - id % 10, talkIndex);
			}
		}

		if (talkIndex == talkData[id].Length)
		{
			return null;
		}
		else
		{
			return talkData[id][talkIndex];
		}
		//예외처리 (안 다듬은 로직)
		/*if (!talkData.ContainsKey(id))
		{
			if (!talkData.ContainsKey(id - id % 10))
			{
				//퀘스트의 처음 대사마저 없을 때
				//기본 대사를 가지고 옴.
				if (talkIndex == talkData[id - id % 100].Length)
				{
					return null;
				}
				else
				{
					return talkData[id - id % 100][talkIndex];
				}
			}
			else
			{
				//해당 퀘스트 진행 순서 대사가 없을 때
				//퀘스트 맨 처음 대사를 가지고 옴.
				if (talkIndex == talkData[id - id % 10].Length)
				{
					return null;
				}
				else
				{
					return talkData[id - id % 10][talkIndex];
				}
			}
		}

		if(talkIndex == talkData[id].Length)
		{
			return null;
		}
		else
		{
			return talkData[id][talkIndex];
		}*/
	}

	public Sprite GetPortrait(int id, int portraitIndex)
	{
		return portraitData[id + portraitIndex];
	}
}

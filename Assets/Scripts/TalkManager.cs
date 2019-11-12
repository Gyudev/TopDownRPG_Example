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
		talkData.Add(11 + 2000, new string[] { "뭐야?:4", "모험가가 되고싶다고?:1", "그럼 어딘가 떨어진 내 동전좀 찾아와.:2" });

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
		if(talkIndex == talkData[id].Length)
		{
			return null;
		}
		else
		{
			return talkData[id][talkIndex];
		}
	}

	public Sprite GetPortrait(int id, int portraitIndex)
	{
		return portraitData[id + portraitIndex];
	}
}

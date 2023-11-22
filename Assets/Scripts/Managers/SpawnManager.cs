using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	public List<Transform> points = new List<Transform>(); // 몬스터가 출현할 위치를 저장할 List 타입 변수
	public List<GameObject> monsterPool = new List<GameObject>(); // 몬스터를 미리 생성해 저장할 리스트 자료형
	public int maxMonsters = 10; // 오브젝트 풀(Object Pool)에 생성할 몬스터의 최대 개수
	public List<GameObject> monster; // 몬스터 프리팹을 연결할 변수
	public float createTime = 1f; // 몬스터의 생성 간격

	[Header("UI")]
	public TMP_Text scoreText;
	public int totalScore;
	public int score;

	void Start()
	{
		// 몬스터 오브젝트 풀 생성
		CreateMonsterPool();

		// SpawnPointGroup 게임오브젝트의 Transform 컴포넌트 추출
		Transform spawnPointGroup = GameObject.Find("SpawnPointGroup")?.transform;

		// SpawnPointGroup 하위에 있는 모든 차일드 게임오브젝트의 Transform 컴포넌트 추출
		foreach (Transform point in spawnPointGroup)
		{
			points.Add(point);
		}

		// 일정한 시간 간격으로 함수를 호출
		InvokeRepeating("CreateMonster", 2.0f, createTime);

		// 스코어 점수 출력
		totalScore = PlayerPrefs.GetInt("Total_Score", 0);
		DisplayScore(0);
	}

	void CreateMonster()
	{
		// 몬스터의 불규칙한 생성 위치 산출
		int index = Random.Range(0, points.Count);

		// 오브젝트 풀에서 몬스터 추출
		GameObject _monster = GetMonsterInPool();
		// 추출한 몬스터의 위치와 회전을 설정
		_monster?.transform.SetPositionAndRotation(points[index].position, points[index].rotation);

		// 추출한 몬스터를 활성화
		_monster?.SetActive(true);
	}

	// 오브젝트 풀에 몬스터 생성
	void CreateMonsterPool()
	{
		for (int i = 0; i < maxMonsters; i++)
		{
			// 초기화
			GameObject _monster = null;

			// 3가지 종류의 몬스터를 생성하여 몬스터 풀에 추가
			if (i % 3 == 0)
			{
				_monster = GameManager.Resource.Instantiate(monster[0]);
				_monster.name = $"MonsterType1_{i / 3:00}";
			}
			else if (i % 3 == 1)
			{
				_monster = GameManager.Resource.Instantiate(monster[1]);
				_monster.name = $"MonsterType2_{i / 3:00}";
			}
			else if (i % 3 == 2)
			{
				_monster = GameManager.Resource.Instantiate(monster[2]);
				_monster.name = $"MonsterType3_{i / 3:00}";
			}

			// 오브젝트 풀에 추가한 몬스터는 생성하자마자 비활성화 함
			// -> 가져와 사용할 때 활성화 함                                         
			_monster.SetActive(false);
			monsterPool.Add(_monster);
		}

	}

	// 오브젝트 풀에서 사용 가능한 몬스터를 추출해 반환하는 함수
	public GameObject GetMonsterInPool()
	{
		// 오브젝트 풀의 처음부터 끝까지 순회
		foreach (var _monster in monsterPool)
		{
			// 비활성화 여부로 사용 가능한 몬스터를 판단
			if (_monster.activeSelf == false)
			{
				// 몬스터 반환5
				return _monster;
			}
		}
		return null;
	}

	public void DisplayScore(int score)
	{
		totalScore += score;
		scoreText.text = "SCORE   :   " + totalScore.ToString();
		PlayerPrefs.SetInt("Total_Score", totalScore);
	}
}

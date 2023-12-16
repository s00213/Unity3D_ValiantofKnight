using Inventories;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
	[Header("PlayerStats")]
	public float curHP;
	public float maxHP;

   [Header("UI")]
	public Slider HPSlider;
	public TMP_Text HPText;

	[Header("")]
	public TMP_Text hitDamageText;
	public bool isDie;

	[Header("")]
	[SerializeField] private string sceneToLoad;

	// 델리게이트 선언
	public delegate void PlayerDieHandler();
	// 이벤트 선언
	public static event PlayerDieHandler OnPlayerDie;

	private Animator animator;

	bool movementStarted = false;

	public void Start()
	{

		HPSlider.maxValue = maxHP;
		curHP = maxHP;
		HPSlider.value = curHP;
		HPText.text = curHP.ToString("f0") + "/" + maxHP.ToString("f0");

		hitDamageText.gameObject.SetActive(false);
		hitDamageText.text = "0";

		isDie = false;
	}

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	private void Update()
	{
		CheckSpecialAbilityKeys();
		if (Input.GetMouseButtonUp(0))
		{
			movementStarted = false;
		}
	}

	// 데미지 받으면 HP 감소
	public void TakeDamage(float damage)
	{
		// 데미지가 현재 HP보다 큰 경우, 데미지를 현재 HP로 설정
		if (damage > curHP)
		{
			damage = curHP;
		}

		curHP -= damage;
		animator.SetTrigger("GetHit");
		HPSlider.value = curHP;

		if (curHP <= 0)
		{
			PlayerDie();
		}

		// 현재 HP와 최대 HP 텍스트로 표시됨
		HPText.text = curHP.ToString("f0") + "/" + maxHP.ToString("f0");
		// 데미지 숫자를 텍스트로 설정
		hitDamageText.text = "-" + damage.ToString();

		StartCoroutine(ShowDamageTextRoutine());
	}

	// 데미지 숫자를 표시하고 일정 시간 후에 비활성화하는 코루틴
	private IEnumerator ShowDamageTextRoutine()
	{
		hitDamageText.gameObject.SetActive(true); // 데미지 숫자 활성화

		yield return new WaitForSeconds(1.0f); // 1초 동안 대기 (데미지 숫자를 표시할 시간)

		hitDamageText.gameObject.SetActive(false); // 데미지 숫자 비활성화
	}

	// 힐 아이템 먹으면 HP 증가
	public void Heal(int healthBoost)
	{
		
	}

	// Player의 사망 처리
	private void PlayerDie()
	{
		Debug.Log("Player Die !");

		// 주인공 사망 이벤트 호출(적이 기뻐함)
		OnPlayerDie();
		isDie = true;

		// 애니메이터에서 "IsDie" 트리거를 설정하여 사망 애니메이션 재생
		Debug.Assert(animator != null);
		animator.SetTrigger("IsDie");

		StartCoroutine(WaitForAnimationToEndRoutine());		
	}

	private IEnumerator WaitForAnimationToEndRoutine()
	{		
		yield return new WaitForSeconds(5f);

		// GameManager 스크립트의 IsGameOver 프로퍼티 값을 변경
		GameManager.Instance.IsGameOver = true;

		// 애니메이션 재생이 끝난 후 게임 오버 씬으로 전환
		GameManager.Scene.LoadScene(sceneToLoad);
		Debug.Log("Enter the " + sceneToLoad);
	}

	// 퀵 슬롯 사용하는 키
	private void CheckSpecialAbilityKeys()
	{
		var actionStore = GetComponent<ActionStore>();
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			actionStore.Use(0, gameObject);
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			actionStore.Use(1, gameObject);
		}
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			actionStore.Use(2, gameObject);
		}
		if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			actionStore.Use(3, gameObject);
		}
		if (Input.GetKeyDown(KeyCode.Alpha5))
		{
			actionStore.Use(4, gameObject);
		}
		if (Input.GetKeyDown(KeyCode.Alpha6))
		{
			actionStore.Use(5, gameObject);
		}
	}
}

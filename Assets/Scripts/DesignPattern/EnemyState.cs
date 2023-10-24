using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;
using UnityEngine.UI;

/// <summary>
/// FSM
/// </summary>

public class EnemyState : MonoBehaviour, IDamageable
{
	// Enemy 상태 정보
	public enum State
	{
		IDLE,
		TRACE,
		ATTACK,
		DIE
	}

	[Header("EnemyStats")]
	public State state = State.IDLE; // Enemy의 현재 상태
	public float traceDistance = 5.0f; // Enemy의 TRACE 가능한 거리
	public float attackDistance = 3.0f; // Enemy의 ATTACK 가능한 거리
	public bool isDie = false; // Enemy의 사망 여부
	[SerializeField] private int HP = 100;
	[SerializeField] private int curHP;

	[Header("UI")]
	[SerializeField] private Slider HPSlider;

	private Transform monster; // Enemy
	private Transform player; // Enemy의 추적 대상
	private NavMeshAgent navMeshAgent;
	private Animator animator;

	private PlayerState playerState; // Player의 데이터 가져옴
	private PlayerAttacker playerAttacker;
	public int damage; // Enemy가 Player에게 가하는 피해

	private void OnEnable()
	{
		HPSlider.maxValue = HP;
		curHP = HP;
		HPSlider.value = curHP;
		state = State.IDLE;

		// 이벤트 발생 시 수행할 함수 연결
		PlayerState.OnPlayerDie += OnPlayerDie;

		// 상태 체크하는 코루틴 시작
		StartCoroutine(EnemyCheckRoutine());
		// 몬수터 동작 수행하는 코루틴 시작
		StartCoroutine(EnemyActionRoutine());
	}

	// 스크립트가 비활성화될 때마다 호출되는 함수
	void OnDisable()
	{
		// 기존에 연결된 함수 해제
		PlayerState.OnPlayerDie -= this.OnPlayerDie;
	}

	private void Awake()
	{
		monster = GetComponent<Transform>();
		player = GetComponent<Transform>();
		navMeshAgent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();

		// 추적 대상 Tag "Player"
		player = GameObject.FindGameObjectWithTag("Player").transform;

		// Player의 데이터를 가져옴
		playerState = FindObjectOfType<PlayerState>();
	}

	void Update()
	{
		HPSlider.value = curHP;
	}

	// 0.1초 간격으로 상태 체크하는 코루틴
	IEnumerator EnemyCheckRoutine()
	{
		while (!isDie)
		{
			// 0.1초 동안 대기
			yield return new WaitForSeconds(0.1f);

			// 몬스터의 상태가 DIE일 때 코루틴을 종료
			if (state == State.DIE)
				yield break;

			// 플레이어와의 거리 측정
			float distance = Vector3.Distance(player.position, monster.position);

			// ATTACK 가능한 범위로 들어왔는지 확인
			if (distance <= attackDistance)
			{
				state = State.ATTACK;
				Debug.Log("ATTACK 범위 확인");
			}
			// TRACE 가능한 범위로 들어왔는지 확인
			else if (distance <= traceDistance)
			{
				state = State.TRACE;
				Debug.Log("TRACE 범위 확인");
			}
			// 아니라면, IDLE
			else
			{
				state = State.IDLE;
				Debug.Log("IDLE");
			}
		}
	}

	// 상태에 따라 동작 수행하는 코루틴
	IEnumerator EnemyActionRoutine()
	{
		// DIE 상태가 아니라면 계속 상태 체크함
		while (!isDie)
		{
			switch (state)
			{
				// IDLE 상태
				case State.IDLE:
					// TRACE 중지
					navMeshAgent.isStopped = true;
					animator.SetBool("IsMove", false);
					Debug.Log("State.IDLE");
					break;

				// TRACE 상태
				case State.TRACE:
					// 추적 시작
					navMeshAgent.SetDestination(player.position);
					navMeshAgent.isStopped = false;
					animator.SetBool("IsMove", true);
					animator.SetBool("IsAttack", false);
					Debug.Log("State.TRACE");
					break;

				// ATTACK 상태
				case State.ATTACK:
					// 플레이어의 위치를 향하도록 몬스터의 회전을 조정
					transform.LookAt(player.position);
					animator.SetBool("IsAttack", true);
					yield return new WaitForSeconds(1f);
					// Player에게 데미지를 줌
					playerState.TakeDamage(damage);
					Debug.Log("State.ATTACK");
					break;

				// DIE 상태
				case State.DIE:
					isDie = true;
					Debug.Log("State.DIE");
					// 추적 정지
					navMeshAgent.isStopped = true;

					// 사망 애니메이션 실행
					animator.SetTrigger("IsDie");

					// 몬스터의 Collider 컴포넌트 비활성화
					GetComponent<Collider>().enabled = false;

					// 일정 시간 대기 후 오브젝트 풀링으로 환원
					yield return new WaitForSeconds(3.0f);

					// 사망 후 다시 사용할 때를 위해 hp 값 초기화
					HP = 100;
					isDie = false;

					// 몬스터의 Collider 컴포넌트 활성화
					GetComponent<Collider>().enabled = true;
					// 몬스터를 비활성화
					this.gameObject.SetActive(false);
					break;
			}
			yield return new WaitForSeconds(0.1f);
		}
	}

	// 플레이어에게 데미지를 입으면 HP 차감되고 피격 리액션 실행됨
	private void OnTriggerEnter(Collider coll)
	{
		if (curHP >= 0.0f && coll.CompareTag("Weapons"))
		{
			curHP -= damage;
			HPSlider.value = curHP;
			// 피격 리액션 애니메이션 실행
			animator.SetTrigger("GetHit");
			Debug.Log($"Enemy HP = {curHP / HP}");

			if (curHP <= 0.0f)
			{
				state = State.DIE;
				// ToDo : 몬스터가 사망했을 때 50 경험치 추가
			}
		}
	}

	// TRACE, ATTACK 기즈모
	private void OnDrawGizmos()
	{
		// TRACE 가능 거리 표시
		if (state == State.TRACE)
		{
			Gizmos.color = Color.blue;
			Gizmos.DrawWireSphere(transform.position, traceDistance);
		}
		// ATTACK 가능 거리 표시
		if (state == State.ATTACK)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(transform.position, attackDistance);
		}
	}

	// 플레이어가 사망했을 때 
	private void OnPlayerDie()
	{
		// 몬스터의 상태를 체크하는 코루틴 함수를 모두 정지시킴
		StopAllCoroutines();

		// 추적을 정지하고 애니메이션을 수행
		navMeshAgent.isStopped = true;
		animator.SetBool("IsMove", false);
		animator.SetTrigger("PlayerDie");
	}

	public void TakeDamage(int damage)
	{
		curHP -= damage;
		if (curHP <= 0) {
			curHP = 0;
			state = State.DIE;
		}
	}
}

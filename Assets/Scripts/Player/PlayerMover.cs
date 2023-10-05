using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
	[Header("Gizmo")]
	[SerializeField] bool debug;

	[Header("Speed")]
	[SerializeField] float walkSpeed;
	[SerializeField] float runSpeed;
	[SerializeField] float jumpSpeed;

	[Header("ViewingRange")]
	[SerializeField] float walkStepRange;
	[SerializeField] float runStepRange;

	private CharacterController characterController;
	private Animator animator;

	private Vector3 moveDirection = Vector3.zero;
	private float currentSpeed;

	private float ySpeed; // ySpeed : 땅에 없으면 떨어지는 속력은 점점 가속시킴 -> 중력처럼 떨어지는 느낌
	private bool walk;

	private void Awake()
	{
		characterController = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
	}

	private void Update()
	{
		Move();
		Jump();
	}

	float lastStepTime = 0.5f;
	private void Move()
	{
		// 이동 중 멈췄을 때 이동 방향이 아니라 왼쪽을 바라보는 것을 방지
		// magnitude : 크기
		if (moveDirection.magnitude == 0)
		{
			currentSpeed = Mathf.Lerp(currentSpeed, 0, 0.1f);
			animator.SetFloat("MoveSpeed", currentSpeed);
			return;
		}

		Vector3 forwardVector = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized;
		Vector3 rightVector = new Vector3(Camera.main.transform.right.x, 0, Camera.main.transform.right.z).normalized;
		// normalized : Vector의 일반화 

		if (walk)
		{
			currentSpeed = Mathf.Lerp(currentSpeed, walkSpeed, 0.1f);
		}
		else
		{
			currentSpeed = Mathf.Lerp(currentSpeed, runSpeed, 0.1f);
		}

		characterController.Move(forwardVector * moveDirection.z * currentSpeed * Time.deltaTime);
		characterController.Move(rightVector * moveDirection.x * currentSpeed * Time.deltaTime);
		animator.SetFloat("MoveSpeed", currentSpeed);

		// 회전
		Quaternion lookRotaion = Quaternion.LookRotation(forwardVector * moveDirection.z + rightVector * moveDirection.x);
		transform.rotation = Quaternion.Lerp(transform.rotation, lookRotaion, 0.1f);
		// Lerp : 선형 보간

		// 적이 들을 수 있는 소리 범위
		lastStepTime -= Time.deltaTime;
		if (lastStepTime < 0)
		{
			lastStepTime = 0.5f;
			GenerateFootStepSound();
		}
	}

	private void GenerateFootStepSound()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, walk ? walkStepRange : runStepRange);
		foreach (Collider collider in colliders)
		{
			IListenable listenable = collider.GetComponent<IListenable>();
			listenable?.Listen(transform);
		}
	}

	private void OnMove(InputValue value)
	{
		// 입력 받은 것은 moveDirection에 잠시 보관
		// 점프하는 방향이 위이므로 방향은 앞, 뒤, 오른쪽, 왼쪽
		// -> x는 x, z는 y로 넣어줌
		moveDirection.x = value.Get<Vector2>().x;
		moveDirection.z = value.Get<Vector2>().y;
	}

	private void OnWalk(InputValue value)
	{
		walk = value.isPressed;
	}

	private void Jump()
	{
		// 중력 방향으로 속력을 더해줌
		ySpeed += Physics.gravity.y * Time.deltaTime;

		// 만약 땅바닥에 붙어 있는 경우에 ySpeed가 -1로 설정해서 위아래로 움직이지 않음
		if (GroundCheck() && ySpeed < 0)
			ySpeed = -1;

		// -> Jump를 뛴 상황이면 위로 속력이 올라감
		// -> Jump를 안 뛴 상황이면 아래로 속력이 떨어짐
		characterController.Move(Vector3.up * ySpeed * Time.deltaTime);
	}

	private void OnJump(InputValue value)
	{
		if (GroundCheck())
			ySpeed = jumpSpeed;
	}

	// 땅바닥에 플레이어가 있을 때만 점프하기 위해 GroundCheck로 확인
	private bool GroundCheck()
	{
		RaycastHit hit;
		return Physics.SphereCast(transform.position + Vector3.up * 1, 0.5f, Vector3.down, out hit, 0.7f);
	}

	private void OnDrawGizmosSelected()
	{
		if (!debug)
			return;

		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere(transform.position, walkSpeed);
		Gizmos.DrawWireSphere(transform.position, runSpeed);
	}
}


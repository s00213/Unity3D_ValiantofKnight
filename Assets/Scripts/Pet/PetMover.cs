using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PetMover : MonoBehaviour
{
	[SerializeField] private float keepDistance;

	private NavMeshAgent agent;
	private Animator animator;
	public Transform player;

	private void Awake()
	{
		agent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();
	}

	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Update()
	{
		Move();
	}

	private void Move()
	{
		animator.SetBool("IsMove", true);

		if (keepDistance > Vector3.Distance(transform.position, player.position))
		{
			animator.SetBool("IsMove", false);
			agent.isStopped = true;
			agent.destination = transform.position;
		}
		else
		{
			animator.SetBool("IsMove", true);
			agent.isStopped = false;
			agent.destination = player.position;
		}
	}
}

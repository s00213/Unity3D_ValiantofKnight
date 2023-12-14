using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventories;
using UnityEngine.AI;

namespace Inventories
{ 
public class ItemRandomDropper : ItemDropper
	{
		[Tooltip("How far can the pickups be scattered from the dropper.")]
		[SerializeField] float scatterDistance = 1;
		[SerializeField] InventoryItem[] dropLibrary;
		[SerializeField] int numberOfDrops = 2;


		const int ATTEMPTS = 30; // 30개의 무작위 포인트 

		public void RandomDrop()
		{
			for (int i = 0; i < numberOfDrops; i++)
			{
				var item = dropLibrary[Random.Range(0, dropLibrary.Length)];
				DropItem(item, 1);
			}
		}

		protected override Vector3 GetDropLocation()
		{
			for (int i = 0; i < ATTEMPTS; i++)
			{
				Vector3 randomPoint = transform.position + Random.insideUnitSphere * scatterDistance;
				NavMeshHit hit;
				if (NavMesh.SamplePosition(randomPoint, out hit, 0.1f, NavMesh.AllAreas))
				{
					return hit.position;
				}
			}
			return transform.position;
		}
	}
}
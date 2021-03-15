using UnityEngine;
using System.Collections;

public class PlayerSight : MonoBehaviour 
{
	public float fieldOfViewAngle = 100f;
	public bool enemyInSight;

	private SphereCollider col;
	private GameObject enemy;

	int enemyMask;

	void Awake()
	{
		col = GetComponent<SphereCollider>();
		enemy = GameObject.FindGameObjectWithTag (Tags.enemy);
		enemyMask = LayerMask.GetMask ("Environment");
	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject == enemy) 
		{
			Debug.Log("InRange");
			enemyInSight=false;
			Vector3 direction = other.transform.position - transform.position;
			float angle = Vector3.Angle(direction, transform.forward);
			if(angle < fieldOfViewAngle * 0.5f)
			{
				Debug.Log("InAngle");
				RaycastHit hit;
				if(Physics.Raycast(transform.position+transform.up , direction.normalized, out hit, col.radius,enemyMask))
				{
					Debug.Log("RayWork");
					Debug.Log (hit.collider);
					if(hit.collider.gameObject == enemy)
					{
						enemyInSight = true;
						Debug.Log("InSight");
					}
				} 
			}
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.gameObject == enemy) {
			enemyInSight = false;
			Debug.Log("OutSight");
		}
	}
}

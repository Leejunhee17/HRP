using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {
	public Transform[] teleportPoints;
	public float coolTime=2.0f;

	private PlayerSight playerSight;
	private float time;
	private int teleportPointIndex;
	private GameObject player;

	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag(Tags.player);
		playerSight = player.GetComponent<PlayerSight>();
		Debug.Log (playerSight);
	}

	void Start ()
	{
		transform.position = teleportPoints [0].position;
	}

	void Update()
	{
		if (playerSight.enemyInSight) {
			time+=Time.deltaTime;
			Debug.Log ("Casting");
			
			if(time >= coolTime)
			{
				Teleporting ();
				time = 0;
			}
		}
	}

	void Teleporting()
	{
		if(teleportPointIndex == teleportPoints.Length - 1)
			teleportPointIndex = 0;
		else
			teleportPointIndex++;
		transform.position=teleportPoints[teleportPointIndex].position;
		Debug.Log ("Teleporting");
	}
}

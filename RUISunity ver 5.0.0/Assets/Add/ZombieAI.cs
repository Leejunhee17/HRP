using UnityEngine;
using System.Collections;

public class ZombieAI : MonoBehaviour {

	public float patrolSpeed = 2f;
	public float runSpeed = 5f;
	public float patrolWaitTime = 1f;
	public float hideWaitTime = 5f;
	public Transform[] patrolWayPoints;
	public Transform hidePoint;
	public float readyTime = 2f;
	public AudioClip watchedAudio;

	private PlayerSight playerSight;
	private GameObject player;
	private NavMeshAgent nav;
	private float hideTimer;
	private float patrolTimer;
	private float readyTimer; 
	private int wayPointIndex;

	bool Hide;
	AudioSource zombieAudio;

	// Use this for initialization
	void Start () {
		transform.position=patrolWayPoints[0].position;
	}

	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag(Tags.player);
		playerSight = player.GetComponent<PlayerSight>();
		nav = GetComponent<NavMeshAgent>();
		zombieAudio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (playerSight.enemyInSight) {
			Hide=true;
			readyTimer+=Time.deltaTime;
			if(readyTimer >= readyTime){
				zombieAudio.clip = watchedAudio;
				zombieAudio.Play();
				Running ();
				readyTimer=0;
			}
		}
		else if (Hide) {
			Running ();
		}
		else
			Patrolling ();
	}

	void Running(){
		nav.speed = runSpeed;
		nav.destination = hidePoint.position;

		if (nav.remainingDistance == 0) {
			Debug.Log ("Hide");
			hideTimer += Time.deltaTime;
			if(hideTimer >= hideWaitTime){
				Hide=false;
				hideTimer=0;
			}
		}
	}

	void Patrolling ()
	{
		nav.speed = patrolSpeed;

		if(nav.remainingDistance < nav.stoppingDistance)
		{
			patrolTimer += Time.deltaTime;

			if(patrolTimer >= patrolWaitTime)
			{
				if(wayPointIndex == patrolWayPoints.Length - 1)
					wayPointIndex = 0;
				else
					wayPointIndex++;

				patrolTimer = 0;
			}
		}
		else
			patrolTimer = 0;

		nav.destination = patrolWayPoints[wayPointIndex].position;
	}
}

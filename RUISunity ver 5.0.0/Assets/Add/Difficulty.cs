using UnityEngine;
using System.Collections;

public class Difficulty : MonoBehaviour {
	public int gameScore=0;
	public int plus=1;
	public int next=3;

	private RUISFistGestureRecognizer shoot1;
	private RUISFistGestureRecognizer spray;
	private Shooting shoot2;
	private Shooting spray2;
	private ZombieAI AI;
	private GameObject skeletonWand;
	private GameObject skeletonWandLeft;
	private GameObject mousewWand;
	private GameObject mousewWandLeft;
	private GameObject enemy;

	public AudioClip hurtAudio;

	public bool flyHurt;
	public bool flySpraied;

	public Vector3 spraiedDisplacement;

	float time=0;
	Vector3 displacement;	

	AudioSource Uaa;
	ParticleSystem Pang;
	Light effect;

	void Awake ()
	{
		skeletonWand = GameObject.FindGameObjectWithTag(Tags.arm);
		shoot1 = skeletonWand.GetComponent<RUISFistGestureRecognizer>();

		enemy = GameObject.FindGameObjectWithTag(Tags.enemy);
		AI = enemy.GetComponent<ZombieAI>();

		mousewWand = GameObject.FindGameObjectWithTag(Tags.mouse);
		shoot2 = mousewWand.GetComponent<Shooting>();

		skeletonWandLeft = GameObject.FindGameObjectWithTag("Armleft");
		spray = skeletonWandLeft.GetComponent<RUISFistGestureRecognizer>();

		mousewWandLeft = GameObject.FindGameObjectWithTag("Mouseleft");
		spray2 = mousewWandLeft.GetComponent<Shooting>();

		Uaa = GetComponent<AudioSource> ();
		Pang = GetComponent<ParticleSystem> ();
		effect = GetComponent<Light> ();

		displacement = FlyBehavior.displacement;
	}



	void Update () 
	{

		time += Time.deltaTime;
		if (time >= 5f) {
			FlyBehavior.displacement = displacement;
		}

		if (shoot2.shooted)
		{
			Debug.Log ("shooted");
			Uaa.clip = hurtAudio;
			Uaa.Play();
			Pang.Stop();
			Pang.Play();

			gameScore+=plus;
			if(next!=0)
			{
				next-=1;
			}
			shoot2.shooted=false;

		}

		if (shoot2.flyShooted)
		{
			Debug.Log ("flyShooted");
			gameScore -= 1;
			
			shoot2.flyShooted=false;
			flyHurt = true;
		}


		if (shoot1.shooted)
		{
			Debug.Log ("shooted");
			Uaa.clip = hurtAudio;
			Uaa.Play();
			Pang.Stop();
			Pang.Play();

			gameScore+=plus;
			if(next!=0)
			{
				next-=1;
			}
			shoot1.shooted=false;
			
		}

		if (shoot1.flyShooted)
		{
			Debug.Log ("flyShooted");
			gameScore -= 1;

			shoot1.flyShooted=false;
			flyHurt = true;
		}

		if (spray2.spraied)
		{
			Debug.Log ("spraied");
			FlyBehavior.displacement = spraiedDisplacement;
			spray2.spraied = false;
			flySpraied = true;
			
			time = 0;
		}


		if (spray.spraied)
		{
			Debug.Log ("spraied");
			FlyBehavior.displacement = spraiedDisplacement;
			spray.spraied = false;
			flySpraied = true;
			
			time = 0;
		}



		if (next==0)
		{
			plus=5;
		}
	}
}

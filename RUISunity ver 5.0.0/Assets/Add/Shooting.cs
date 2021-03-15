using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour {
	
	public float timeBetweenBullets=0.50f;
	public float range=100f;
	float timer;
	LineRenderer gunLine;
	float effectDisplayTime=0.2f;
	Ray shootRay;
	RaycastHit shootHit;
	int shootableMask;
	ParticleSystem gunParticles;
	AudioSource gunAudio; 
	Light gunLight;
	RUISSelectable obj;
	public bool shooted;
	public bool flyShooted;
	public bool spraied;


	void Awake()
	{
		gunParticles = GetComponent<ParticleSystem> ();
		gunLine = GetComponent <LineRenderer> ();
		gunAudio = GetComponent<AudioSource> ();
		gunLight = GetComponent<Light> ();
		shootableMask = LayerMask.GetMask ("Environment");


	}
	
	void Shoot()
	{
		timer = 0f;
		gunAudio.Play ();
		gunLight.enabled = true;
		gunParticles.Stop ();
		gunParticles.Play ();
		gunLine.enabled = true;
		gunLine.SetPosition (0, transform.position);
		shootRay.origin = transform.position;
		shootRay.direction = transform.forward;
		if(Physics.Raycast (shootRay, out shootHit, range)) 
		{
			gunLine.SetPosition (1, shootHit.point);
			Debug.Log ("shooted");
			if (shootHit.collider.tag == "Enemy") 
			{
				shooted = true;
			}
			else if (shootHit.collider.tag == "Fly") 
			{
				flyShooted = true;
			}
		}
		else
		{
			gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
		}

	}

	void Spray()
	{
		timer = 0f;
		gunAudio.Play ();
		gunLight.enabled = true;
		gunParticles.Stop ();
		gunParticles.Play ();
		gunLine.enabled = true;
		gunLine.SetPosition (0, transform.position);
		shootRay.origin = transform.position;
		shootRay.direction = transform.forward;
		if (Physics.Raycast (shootRay, out shootHit, range)) {	
			if (shootHit.collider.tag == "Enemy" || shootHit.collider.tag == "Fly") {
				spraied = true;
				Debug.Log ("spraied");
			}
			
		}
		else
		{
			gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
		}
	}

	void Update () {
		timer += Time.deltaTime;
		if(Input.GetMouseButtonDown(0) && timer >= timeBetweenBullets && Time.timeScale != 0 && CompareTag("Mouse"))
		{
			Debug.Log ("shoot");
			Shoot ();
		}

		else if(Input.GetMouseButtonDown(1) && timer >= timeBetweenBullets && Time.timeScale != 0 && CompareTag("Mouseleft"))
		{
			Debug.Log ("spray");
			Spray();
		}

		if (timer >= timeBetweenBullets * effectDisplayTime) 
		{
			DisableEffects();
		}
	}
	public void DisableEffects()
	{
		gunLine.enabled = false;
		gunLight.enabled = false;
	}
}

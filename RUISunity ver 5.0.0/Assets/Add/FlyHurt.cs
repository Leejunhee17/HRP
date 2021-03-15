using UnityEngine;
using System.Collections;

public class FlyHurt : MonoBehaviour {
	
	private Difficulty diff;
	private GameObject enemy;
	AudioSource flyHurtAudio;
	ParticleSystem flyHurtParticle;
	Transform now;
	
	// Use this for initialization
	void Awake () {
		enemy = enemy = GameObject.FindGameObjectWithTag("Enemy");
		diff = enemy.GetComponent<Difficulty> ();
		flyHurtAudio = GetComponent<AudioSource> ();
		flyHurtParticle = GetComponent<ParticleSystem> ();

		now = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		now.position = new Vector3 (enemy.transform.position.x, enemy.transform.position.y + 2f, enemy.transform.position.z);
		
		if (diff.flyHurt) {
			flyHurtAudio.Play ();
			flyHurtParticle.Stop ();
			flyHurtParticle.Play ();

			diff.flyHurt = false;
		}
	}
}

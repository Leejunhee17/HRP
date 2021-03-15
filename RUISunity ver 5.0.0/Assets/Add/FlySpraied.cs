using UnityEngine;
using System.Collections;

public class FlySpraied : MonoBehaviour {
	
	private Difficulty diff;
	private GameObject enemy;
	AudioSource flySpraiedAudio;
	Transform now;
	
	// Use this for initialization
	void Awake () {
		enemy = enemy = GameObject.FindGameObjectWithTag("Enemy");
		diff = enemy.GetComponent<Difficulty> ();
		flySpraiedAudio = GetComponent<AudioSource> ();
		
		now = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		now.position = enemy.transform.position;
		
		if (diff.flySpraied) {
			flySpraiedAudio.Play ();
			diff.flySpraied = false;
		}
	}
}

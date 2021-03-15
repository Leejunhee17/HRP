using UnityEngine;
using System.Collections;

public class ScoreText : MonoBehaviour {
	TextMesh textt;
	GameObject chest;
	Difficulty diff;
	int score;
	
	// Use this for initialization
	void Start () {
		chest = GameObject.FindGameObjectWithTag("Enemy");
		diff = chest.GetComponent<Difficulty>();
		
		textt = GetComponent<TextMesh> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		score = diff.gameScore;
		textt.text = "Score\n"+score;
	}
}

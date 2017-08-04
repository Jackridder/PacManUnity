using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacDot : MonoBehaviour {
	//Highscore
	public static int score = 0;
	public GameBehaviour game;
	void OnTriggerEnter2D(Collider2D co){
		if (co.name == "PacMan") {
			score++;
			game.eatenDot (name);
			Destroy (gameObject);
		}
		if (tag == "BigDot" && co.name == "PacMan") {
			GhostMove.bigDot = true;
		}
	}
}

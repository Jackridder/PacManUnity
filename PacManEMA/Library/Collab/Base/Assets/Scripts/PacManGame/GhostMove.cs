using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GhostMove : MonoBehaviour {
	public Transform[] waypoints;
	int cur = 0;
	bool start = false;
	public float speed = 0.3f;
	public GameBehaviour game;
	IEnumerator Start()
	{
		//Part1
		yield return new WaitForSeconds(4);
		start = true;
		//Part2

	}

	void FixedUpdate(){
		if (start) {
			if(transform.position != waypoints[cur].position) {
				Vector2 p = Vector2.MoveTowards(transform.position,
					waypoints[cur].position,
					speed);
				GetComponent<Rigidbody2D>().MovePosition(p);

			}
			else cur = (cur + 1) % waypoints.Length;
			Vector2 dir = waypoints [cur].position - transform.position;
			GetComponent<Animator>().SetFloat("DirX", dir.x);
			GetComponent<Animator>().SetFloat("DirY", dir.y);
		}
	}

	public void resetGhost(){
		transform.position = new Vector2(15, 17);
		GetComponent<Animator>().SetFloat("DirX", 1);
		GetComponent<Animator>().SetFloat("DirY", 0);
		start = false;
		cur = 0;
		IEnumerator coroutine = ghostWait(4);
		StartCoroutine(coroutine);
	}

	IEnumerator ghostWait(float timer){
		while (true) {
			yield return new WaitForSeconds (timer);
			start = true;
		}
	}

	void OnTriggerEnter2D(Collider2D co){
		if (co.name == "PacMan") {
			Destroy (co.gameObject);
			game.startLifeLost ();
		}
	}
}

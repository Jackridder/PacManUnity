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
	private Vector2 oldPos = Vector2.zero;
	IEnumerator Start()
	{

		yield return new WaitForSeconds(4);
		start = true;

	}

	void FixedUpdate(){
		if (start) {
			transform.position = new Vector2 (GameObject.Find ("BlinkyAgent").transform.position.x, GameObject.Find ("BlinkyAgent").transform.position.z + 15);
			Vector2 dir = Vector2.zero;
			//Debug.Log ("X Pos: " + (oldPos.x - transform.position.x));
			Debug.Log ("Y Pos: " + (oldPos.y - transform.position.y));
			if (oldPos.x - transform.position.x < 0 && Mathf.Abs(oldPos.x-transform.position.x) - Mathf.Abs(oldPos.y-transform.position.y) > 0)
				dir = new Vector2 (1, 0);
			if (oldPos.x - transform.position.x > 0) {
				dir = new Vector2 (-1, 0);
			}
			//runter Pos
			//hoch negativ
			//links neg
			//rechts pos

			GetComponent<Animator>().SetFloat("DirX", dir.x);
			GetComponent<Animator>().SetFloat("DirY", dir.y);
			/*
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
			*/
			oldPos = transform.position;
		}
	}

	void OnTriggerEnter2D(Collider2D co){
		if (co.name == "PacMan") {
			Destroy (co.gameObject);
			game.startLifeLost ();
		}
	}
}

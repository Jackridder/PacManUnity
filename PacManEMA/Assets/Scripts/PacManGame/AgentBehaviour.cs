using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentBehaviour : MonoBehaviour {

	private NavMeshAgent agent;
	private bool start = false;
	private int ghost = 0;
	private bool lockClyde = false;
	private Vector3 pacPos = Vector3.zero;
	public static bool lockGhosts = false;
	private Transform ghostPos;
	private int counter = 0;

	IEnumerator Start()
	{
		ghostPos = transform;
		//Which Ghost is selected
		selectGhost ();
		//Set Agent to Ghosts NavMeshAgent
		agent = GetComponent<NavMeshAgent> ();
		//Wait a few seconds till Ghost can move
		yield return new WaitForSeconds(ghost == 1 ? 4 : ghost == 2 ? 5 : ghost == 3 ? 6 : 6.5f);
		start = true;

	}

	// Update is called once per frame
	void FixedUpdate () {
		//Blinky:
		if (start && GameObject.Find("PacMan") != null && ghost == 1 && !lockGhosts) {
			//Where is PacMan?
			Ray ray = new Ray(new Vector3(GameObject.Find("PacMan").transform.position.x,transform.position.y,GameObject.Find("PacMan").transform.position.y-15),new Vector3(0,-1,0));
			RaycastHit hit;
			//Ray on ground?
			if (Physics.Raycast (ray, out hit)) {
				//Than Move
				if(hit.collider.name == "Ground")
					agent.SetDestination (hit.point);
			}
			ghostPos.position = transform.position;
		}
		//Clyde
		else if (start && GameObject.Find("PacMan") != null && ghost == 3 && !lockGhosts) {
			Ray ray;
			RaycastHit hit;
			//Where is PacMans last position?
			if (!lockClyde) {
				//Where is PacMan?
				pacPos = new Vector3 (GameObject.Find ("PacMan").transform.position.x, transform.position.y, GameObject.Find ("PacMan").transform.position.y - 15);
				ray = new Ray(pacPos,new Vector3(0,-1,0));
				//Ray on Ground?
				if (Physics.Raycast (ray, out hit)) {
					//Than Move
					if(hit.collider.name == "Ground")
						agent.SetDestination (hit.point);
				}
				lockClyde = true;

			}
			else if (lockClyde) {
				//Go to PacMans last position
				ray = new Ray(pacPos,new Vector3(0,-1,0));
				//Ray on Ground?
				if (Physics.Raycast (ray, out hit)) {
					//Than Move
					if(hit.collider.name == "Ground")
						agent.SetDestination (hit.point);
				}
				//Last position reached: Search new "last position"
				if (Mathf.Round(transform.position.x) == Mathf.Round(pacPos.x) && Mathf.Round(transform.position.z) == Mathf.Round(pacPos.z)) {
					lockClyde = false;
				}
			}
			ghostPos.position = transform.position;
		}
		if (lockGhosts) {
			if(ghost == 1)
				transform.position = new Vector3(GameObject.Find ("blinky").transform.position.x,transform.position.y, GameObject.Find ("blinky").transform.position.y-15);
			else if(ghost == 3)
				transform.position = new Vector3(GameObject.Find ("clyde").transform.position.x,transform.position.y, GameObject.Find ("clyde").transform.position.y-15);
		} else {
			//transform.position = ghostPos.position;
		}
	}

	void selectGhost(){
		if (name == "BlinkyAgent")
			ghost = 1;
		if (name == "InkyAgent")
			ghost = 2;
		if (name == "ClydeAgent")
			ghost = 3;
		if (name == "PinkyAgent")
			ghost = 4;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GhostMove : MonoBehaviour {
	public Transform[] waypoints;
	private GameObject waypoint;
	public GameObject ghostBigDotBlinky;
	public GameObject ghostBigDotClyde;
	public GameObject ghostBigDotInky;
	public GameObject ghostBigDotPinky;
	public bool start = false;
	public GameBehaviour game;
	private Vector2 oldPos = Vector2.zero;
	private int ghost = 0;
	private int randVal = 0;
	private int currentMov = 0;
	private int counter = 0;
	private int cur = 0;
	private float speed = 0.3f;
	private bool wayPointReached = false;
	public static bool bigDot = false;
	private IEnumerator coroutine;
	IEnumerator Start()
	{
		//which ghost is selected?
		selectGhost ();
		//random movement code
		//if (ghost == 2) {
		//	waypoints = new Transform[2];
		//}
		//wait time for each ghost
		yield return new WaitForSeconds(ghost == 1 ? 4 : ghost == 2 ? 5 : ghost == 3 ? 6 : 6.5f);
		start = true;

	}

	void FixedUpdate(){
		if (start && !bigDot) {
			StopAllCoroutines ();
			//Blinky
			if (ghost == 1) {
				//calc 3D to 2D position
				transform.position = new Vector2 (GameObject.Find ("BlinkyAgent").transform.position.x, GameObject.Find ("BlinkyAgent").transform.position.z + 15);
				//select animation direction
				Vector2 dir = Vector2.zero;
				if (oldPos.x - transform.position.x < -0.1f && Mathf.Abs (oldPos.x - transform.position.x) - Mathf.Abs (oldPos.y - transform.position.y) > 0)
					dir = new Vector2 (1, 0);
				else if (oldPos.x - transform.position.x > 0.1f && Mathf.Abs (oldPos.x - transform.position.x) - Mathf.Abs (oldPos.y - transform.position.y) > 0) {
					dir = new Vector2 (-1, 0);
				} else if (oldPos.y - transform.position.y > 0.1f && Mathf.Abs (oldPos.y - transform.position.y) - Mathf.Abs (oldPos.x - transform.position.x) > 0) {
					dir = new Vector2 (0, -1);
				} else if (oldPos.y - transform.position.y < -0.1f && Mathf.Abs (oldPos.y - transform.position.y) - Mathf.Abs (oldPos.x - transform.position.x) > 0) {
					dir = new Vector2 (0, 1);
				}
				GetComponent<Animator> ().SetFloat ("DirX", dir.x);
				GetComponent<Animator> ().SetFloat ("DirY", dir.y);
				oldPos = transform.position;
			} else if (ghost == 2) {
				//inky:
				speed = 0.25f;
				if(transform.position != waypoints[cur].position) {
					Vector2 p = Vector2.MoveTowards(transform.position,
						waypoints[cur].position,
						speed);
					GetComponent<Rigidbody2D>().MovePosition(p);

				}
				//set new waypoint or start waypoints again
				else cur = (cur + 1) % waypoints.Length;
				//select animation direction
				Vector2 dir = waypoints [cur].position - transform.position;
				GetComponent<Animator>().SetFloat("DirX", dir.x);
				GetComponent<Animator>().SetFloat("DirY", dir.y);
				//Random movement code (does not work now)
				/*if(waypoints[1] != null && Mathf.Round(transform.position.x) == Mathf.Round(waypoints[1].transform.position.x) && Mathf.Round(transform.position.y) == Mathf.Round(waypoints[1].transform.position.y)){
					wayPointReached = true;
					//Destroy (waypoint);
				}
				if (waypoints[0] == null || wayPointReached) {
					waypoint = new GameObject ();
					waypoint.transform.position = new Vector2 (Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
					waypoints [0] = waypoint.transform;
					Destroy (waypoint);
					waypoint = new GameObject ();
					int calcPosMoves = 0;
					//1 UP, 2 DOWN, 4 LEFT, 8 RIGHT
					if (validWayPoint (new Vector2 ((int)transform.position.x, (int)transform.position.y), Vector2.right)){// && curPos != 4) {
						calcPosMoves += 8;
					}
					if (validWayPoint (new Vector2 ((int)transform.position.x, (int)transform.position.y), -Vector2.right)){// && curPos != 8) {
						calcPosMoves += 4;
					}
					if (validWayPoint (new Vector2 ((int)transform.position.x, (int)transform.position.y), Vector2.up)){// && curPos != 2) {
						calcPosMoves += 1;
					}
					if (validWayPoint (new Vector2 ((int)transform.position.x, (int)transform.position.y), -Vector2.up)){//&& curPos != 1) {
						calcPosMoves += 2;
					}

					getDir (calcPosMoves);

					waypoints [1] = waypoint.transform;
					Destroy (waypoint);
					wayPointReached = false;
					oldPos = transform.position;
				}
				if (transform.position != waypoints [cur].position) {
					Vector2 p = Vector2.MoveTowards (transform.position,
						            waypoints [cur].position,
						            speed);
					GetComponent<Rigidbody2D> ().MovePosition (p);
				} else {
					cur = (cur + 1) % waypoints.Length;
				}*/
			} else if (ghost == 3) {
				//clyde:
				//calc 3D to 2D position
				transform.position = new Vector2 (GameObject.Find ("ClydeAgent").transform.position.x, GameObject.Find ("ClydeAgent").transform.position.z + 15);
				//select animation direction
				Vector2 dir = Vector2.zero;
				if (oldPos.x - transform.position.x < -0.1f && Mathf.Abs (oldPos.x - transform.position.x) - Mathf.Abs (oldPos.y - transform.position.y) > 0)
					dir = new Vector2 (1, 0);
				else if (oldPos.x - transform.position.x > 0.1f && Mathf.Abs (oldPos.x - transform.position.x) - Mathf.Abs (oldPos.y - transform.position.y) > 0) {
					dir = new Vector2 (-1, 0);
				} else if (oldPos.y - transform.position.y > 0.1f && Mathf.Abs (oldPos.y - transform.position.y) - Mathf.Abs (oldPos.x - transform.position.x) > 0) {
					dir = new Vector2 (0, -1);
				} else if (oldPos.y - transform.position.y < -0.1f && Mathf.Abs (oldPos.y - transform.position.y) - Mathf.Abs (oldPos.x - transform.position.x) > 0) {
					dir = new Vector2 (0, 1);
				}
				GetComponent<Animator> ().SetFloat ("DirX", dir.x);
				GetComponent<Animator> ().SetFloat ("DirY", dir.y);
				oldPos = transform.position;
			} else {
				//pinky:
				//go to waypoint till waypoint reached
				if(transform.position != waypoints[cur].position) {
					Vector2 p = Vector2.MoveTowards(transform.position,
						waypoints[cur].position,
						speed);
					GetComponent<Rigidbody2D>().MovePosition(p);

				}
				//set new waypoint or start waypoints again
				else cur = (cur + 1) % waypoints.Length;
				//select animation direction
				Vector2 dir = waypoints [cur].position - transform.position;
				GetComponent<Animator>().SetFloat("DirX", dir.x);
				GetComponent<Animator>().SetFloat("DirY", dir.y);
			}
		}
		if (bigDot) {
			//Disable ghost move Animation
			GetComponent<SpriteRenderer> ().enabled = false;
			//Lock Ghosts
			AgentBehaviour.lockGhosts = true;
			//Acivate ghost animation for selected ghost and set animation position to current ghost positon
			if (ghost == 1) {
				ghostBigDotBlinky.SetActive (true);
				ghostBigDotBlinky.transform.position = transform.position;
			}else if (ghost == 2) {
				ghostBigDotInky.SetActive (true);
				ghostBigDotInky.transform.position = transform.position;
			}else if (ghost == 3) {
				ghostBigDotClyde.SetActive (true);
				ghostBigDotClyde.transform.position = transform.position;
			}
			else if (ghost == 4) {
				ghostBigDotPinky.SetActive (true);
				ghostBigDotPinky.transform.position = transform.position;
			}
			//Ghost sleep for 2 seconds
			coroutine = wait(2);
			StartCoroutine(coroutine);
		}
	}

	private IEnumerator wait(float waitTime)
	{
		while (bigDot)
		{
			yield return new WaitForSeconds(waitTime);
			//After 2 seconds reset everything
			bigDot = false;
			if (ghost == 1)
				ghostBigDotBlinky.SetActive (false);
			else if( ghost == 2)
				ghostBigDotInky.SetActive(false);
			else if( ghost == 3)
				ghostBigDotClyde.SetActive(false);
			else if( ghost == 4)
				ghostBigDotPinky.SetActive(false);
			GetComponent<SpriteRenderer> ().enabled = true;
			AgentBehaviour.lockGhosts = false;
		}
	}

	void OnTriggerEnter2D(Collider2D co){
		//if ghost collided with PacMan start life lost and destroy pacman object
		if (co.name == "PacMan") {
			game.startLifeLost ();
			Destroy (co.gameObject);
		}
	}

	//Random movement code (does not work now)
	void selectGhost(){
		if (name == "blinky")
			ghost = 1;
		if (name == "inky")
			ghost = 2;
		if (name == "clyde")
			ghost = 3;
		if (name == "pinky")
			ghost = 4;
	}

	void getDir(int choices){
		//1 UP, 2 DOWN, 4 LEFT, 8 RIGHT
		switch (choices){
		case 1:
			moveUp ();
			break;
		case 2: 
			moveDown ();
			break;
		case 3: 
			randVal = (int)(Random.value*10%2)+1;
			if (randVal == 1 && currentMov != 2)
				moveUp ();
			else moveDown();
			break;
		case 4:
			moveLeft ();
			break;
		case 5:
			randVal = (int)(Random.value*10%2)+1;
			if (randVal == 1 && currentMov != 8)
				moveLeft ();
			else moveUp();
			break;
		case 6:
			randVal = (int)(Random.value*10%2)+1; 
			if (randVal == 1 && currentMov != 8)
				moveLeft ();
			else moveDown();
			break;
		case 7:
			randVal = (int)(Random.value*12%3)+1;
			if (randVal == 1 && currentMov != 8)
				moveLeft ();
			else if (randVal == 2 && currentMov != 2)
				moveUp ();
			else moveDown();
			break;
		case 8:
			moveRight ();
			break;
		case 9:
			randVal = (int)(Random.value*10%2)+1;
			if (randVal == 1 && currentMov != 4)
				moveRight ();
			else moveUp();
			break;
		case 10:
			randVal =(int)(Random.value*10%2)+1;
			if (randVal == 1 && currentMov != 4)
				moveRight ();
			else moveDown();
			break;
		case 11:
			randVal = (int)(Random.value*12%3)+1;
			if (randVal == 1 && currentMov != 4)
				moveRight ();
			else if (randVal == 2 && currentMov != 2)
				moveUp ();
			else moveDown();
			break;
		case 12: 
			randVal = (int)(Random.value*10%2)+1;
			if (randVal == 1 && currentMov != 4)
				moveRight ();
			else moveLeft();
			break;
		case 13: 
			randVal = (int)(Random.value*12%3)+1;
			if (randVal == 1 && currentMov != 4)
				moveRight ();
			else if (randVal == 2 && currentMov != 2)
				moveUp ();
			else moveLeft();
			break;
		case 14: 
			randVal = (int)(Random.value*12%3)+1;
			if (randVal == 1 && currentMov !=4)
				moveRight ();
			else if (randVal == 2 && currentMov != 1)
				moveDown ();
			else moveLeft();
			break;
		case 15: 
			randVal = (int)(Random.value * 16 % 4) + 1;
			if (randVal == 1 && currentMov != 4)
				moveRight ();
			else if (randVal == 2 && currentMov != 1)
				moveDown ();
			else if (randVal == 3 && currentMov != 8)
				moveLeft ();
			else
				moveUp ();
			break;
		}
	}
		
	//1 UP, 2 DOWN, 4 LEFT, 8 RIGHT
	void moveLeft(){
		waypoint.transform.position = new Vector2 ((int)transform.position.x - 1, (int)transform.position.y);
		currentMov = 4;
	}

	void moveRight(){
		waypoint.transform.position = new Vector2 ((int)transform.position.x + 1, (int)transform.position.y);
		currentMov = 8;
	}

	void moveUp(){
		waypoint.transform.position = new Vector2 ((int)transform.position.x, (int)transform.position.y+1);
		currentMov = 1;
	}

	void moveDown(){
		waypoint.transform.position = new Vector2 ((int)transform.position.x, (int)transform.position.y-1);
		currentMov = 2;
	}

	private bool valid(Vector2 dir){
		Vector2 pos = (Vector2)transform.position;
		RaycastHit2D hit = Physics2D.Linecast (pos + dir, pos);
		return !(hit.collider.name == "maze");
	}
	private bool validWayPoint(Vector2 pos, Vector2 dir){
		RaycastHit2D hit = Physics2D.Linecast ( pos + dir, pos);
		return !(hit.collider.name == "maze");
	}
}


/* Random Movement: not working now
 * else if (ghost == 2) {
				if(waypoints[1] != null &&Mathf.Round(transform.position.x) == Mathf.Round(waypoints[1].transform.position.x) && Mathf.Round(transform.position.y) == Mathf.Round(waypoints[1].transform.position.y)){
					wayPointReached = true;
				}
				if (waypoints[0] == null || wayPointReached) {
					//waypoints = new Transform[2];
					GameObject waypoint = new GameObject ();
					waypoint.transform.position = new Vector2 (transform.position.x, transform.position.y);
					waypoints [0] = waypoint.transform;
					waypoint = new GameObject ();
					//(int)(Random.value*10%2)+1
					if (validWayPoint (new Vector2 (transform.position.x, transform.position.y), Vector2.right)) {
						waypoint.transform.position = new Vector2 ((int)transform.position.x + 1, transform.position.y);
					} else if (validWayPoint (new Vector2 (transform.position.x, transform.position.y),Vector2.up)) {
						waypoint.transform.position = new Vector2 (transform.position.x, (int)transform.position.y+1);
					}
					//waypoint.transform.position = new Vector2 (10, 15);
					waypoints [1] = waypoint.transform;
					wayPointReached = false;
				}
				if (transform.position != waypoints [cur].position) {
					Vector2 p = Vector2.MoveTowards (transform.position,
						           waypoints [cur].position,
						           speed);
					GetComponent<Rigidbody2D> ().MovePosition (p);
				} else cur = (cur + 1) % waypoints.Length;


				/*Vector2 dir = waypoints [cur].position - transform.position;
				GetComponent<Animator>().SetFloat("DirX", dir.x);
				GetComponent<Animator>().SetFloat("DirY", dir.y);

if (randVal == 0) {
	transform.position = new Vector2 (15, 20);
}
int choices = 0;
if (valid (Vector2.up)) {
	choices += 1;
}
if (valid (-Vector2.up)) {
	choices += 2;
}
if (valid (-Vector2.right)) {
	choices += 4;
}
if (valid (Vector2.right)) {
	choices += 8;
}
if (counter < 10) {
	getDir (currentMov);
	counter++;
} else {
	transform.position = new Vector2 (Mathf.Round (transform.position.x), Mathf.Round (transform.position.y));
	counter = 0;
	getDir (choices);
}
Vector2 dir = Vector2.zero;
if (oldPos.x - transform.position.x < -0 && Mathf.Abs (oldPos.x - transform.position.x) - Mathf.Abs (oldPos.y - transform.position.y) > 0)
	dir = new Vector2 (1, 0);
else if (oldPos.x - transform.position.x > 0 && Mathf.Abs (oldPos.x - transform.position.x) - Mathf.Abs (oldPos.y - transform.position.y) > 0) {
	dir = new Vector2 (-1, 0);
} else if (oldPos.y - transform.position.y > 0 && Mathf.Abs (oldPos.y - transform.position.y) - Mathf.Abs (oldPos.x - transform.position.x) > 0) {
	dir = new Vector2 (0, -1);
} else if (oldPos.y - transform.position.y < -0 && Mathf.Abs (oldPos.y - transform.position.y) - Mathf.Abs (oldPos.x - transform.position.x) > 0) {
	dir = new Vector2 (0, 1);
}
GetComponent<Animator> ().SetFloat ("DirX", dir.x);
GetComponent<Animator> ().SetFloat ("DirY", dir.y);
oldPos = transform.position;

void getDir(int choices){
		//1 UP, 2 DOWN, 4 LEFT, 8 RIGHT
		switch (choices){
		case 1:
			moveUp ();
			break;
		case 2: 
			moveDown ();
			break;
		case 3: 
			randVal = (int)(Random.value*10%2)+1;
			if (randVal == 1 && currentMov != 2)
				moveUp ();
			else moveDown();
			break;
		case 4:
			moveLeft ();
			break;
		case 5:
			randVal = (int)(Random.value*10%2)+1;
			if (randVal == 1 && currentMov != 8)
				moveLeft ();
			else moveUp();
			break;
		case 6:
			randVal = (int)(Random.value*10%2)+1; 
			if (randVal == 1 && currentMov != 8)
				moveLeft ();
			else moveDown();
			break;
		case 7:
			randVal = (int)(Random.value*10%3)+1;
			if (randVal == 1 && currentMov != 8)
				moveLeft ();
			else if (randVal == 2 && currentMov != 2)
				moveUp ();
			else moveDown();
			break;
		case 8:
			moveRight ();
			break;
		case 9:
			Debug.Log ("9");
			randVal = (int)(Random.value*10%2)+1;
			if (randVal == 1 && currentMov != 4)
				moveRight ();
			else moveUp();
			break;
		case 10:
			randVal =(int)(Random.value*10%2)+1;
			if (randVal == 1 && currentMov != 4)
				moveRight ();
			else moveDown();
			break;
		case 11:
			randVal = (int)(Random.value*10%3)+1;
			if (randVal == 1 && currentMov != 4)
				moveRight ();
			else if (randVal == 2 && currentMov != 2)
				moveUp ();
			else moveDown();
			break;
		case 12: 
			randVal = (int)(Random.value*10%2)+1;
			if (randVal == 1 && currentMov != 4)
				moveRight ();
			else moveLeft();
			break;
		case 13: 
			randVal = (int)(Random.value*10%3)+1;
			if (randVal == 1 && currentMov != 4)
				moveRight ();
			else if (randVal == 2 && currentMov != 2)
				moveUp ();
			else moveLeft();
			break;
		case 14: 
			randVal = (int)(Random.value*12%3)+1;
			if (randVal == 1 && currentMov !=4)
				moveRight ();
			else if (randVal == 2 && currentMov != 1)
				moveDown ();
			else moveLeft();
			break;
		case 15: 
			randVal = (int)(Random.value * 12 % 4) + 1;
			if (randVal == 1 && currentMov != 4)
				moveRight ();
			else if (randVal == 2 && currentMov != 1)
				moveDown ();
			else if (randVal == 3 && currentMov != 8)
				moveLeft ();
			else
				moveUp ();
			break;
		}
	}

void moveLeft(){
		transform.position = (Vector2)transform.position - (Vector2.right*0.2f);
		currentMov = 4;
	}

	void moveRight(){
		transform.position = (Vector2)transform.position + (Vector2.right*0.2f);
		currentMov = 8;
	}

	void moveUp(){
		transform.position = (Vector2)transform.position + (Vector2.up*0.2f);
		currentMov = 1;
	}

	void moveDown(){
		transform.position = (Vector2)transform.position - (Vector2.up*0.2f);
		currentMov = 2;
	}
*/
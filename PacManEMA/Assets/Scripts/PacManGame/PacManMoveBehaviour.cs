using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacManMoveBehaviour : MonoBehaviour {
	public float speed = 0.4f;
	private Vector2 dest = Vector2.zero;
	private Vector2 oldMov = Vector2.zero;
	private Vector2 firstPressPos;
	private Vector2 secondPressPos;
	private Vector2 currentSwipe;
	public int moveWish = 0;
	public int currentMove = 0;
	// Use this for initialization
	void Start () {
		//set Pacmans animation clip
		GetComponent<Animator> ().SetFloat ("DirX", 0);
		GetComponent<Animator> ().SetFloat ("DirY", 0);
		//save pacmans position for movement
		dest = transform.position;
		
	}

	// Update is called once per frame
	void FixedUpdate () {
		//move Pacman to calculated point
		Vector2 p = Vector2.MoveTowards (transform.position, dest, speed);
		GetComponent<Rigidbody2D> ().MovePosition (p);

		//Touch:
		if (Input.touches.Length > 0) {
			Touch t = Input.GetTouch (0);
			//Where started the touch input?
			if (t.phase == TouchPhase.Began) {
				firstPressPos = new Vector2 (t.position.x, t.position.y);
			}
			//where ended the touch input?
			if(t.phase == TouchPhase.Ended){
				secondPressPos = new Vector2(t.position.x,t.position.y);
				//calc swipe direction
				currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
				currentSwipe.Normalize();
				//swipe upwards
				if(currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f){
					Debug.Log("UP");
					moveWish = 1;
				}
				//swipe down
				if(currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f){
					Debug.Log("DOWN");
					moveWish = 2;
				}
				//swipe left
				if(currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f){
					Debug.Log("LEFT");
					moveWish = 3;
				}
				//swipe right
				if(currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f){
					Debug.Log("RIGHT");
					moveWish = 4;
				}
			}
		}
		moveRecognizer ();

		//KeyBoard
		if ((Vector2)transform.position == dest) {
			if (Input.GetKey(KeyCode.UpArrow) && valid(Vector2.up))
				dest = (Vector2)transform.position + Vector2.up;
			if (Input.GetKey(KeyCode.RightArrow) && valid(Vector2.right))
				dest = (Vector2)transform.position + Vector2.right;
			if (Input.GetKey(KeyCode.DownArrow) && valid(-Vector2.up))
				dest = (Vector2)transform.position - Vector2.up;
			if (Input.GetKey(KeyCode.LeftArrow) && valid(-Vector2.right))
				dest = (Vector2)transform.position - Vector2.right;
		}

		//(new) animation direction
		Vector2 dir = dest - (Vector2)transform.position;
		GetComponent<Animator>().SetFloat("DirX", dir.x);
		GetComponent<Animator>().SetFloat("DirY", dir.y);
	}

	private void moveRecognizer(){
		//pacman only can move if there is a way in swiped direction
		if (moveWish != 0 && Vector2.Distance(dest,transform.position) < 0.00001f) {
			//1 UP, 2 DOWN, 3 LEFT, 4 RIGHT
			Vector2 nextPosMov = moveWish == 1 ? Vector2.up : moveWish == 2 ? -Vector2.up : moveWish == 3 ? -Vector2.right : Vector2.right;
			if (valid (nextPosMov)) {
				dest = (Vector2)transform.position + nextPosMov;
				oldMov = nextPosMov;
			} else 
				if(valid(oldMov)) dest = (Vector2)transform.position + oldMov;
			
		}
	}
		
	private bool valid(Vector2 dir){
		//collide with maze or something else? return false
		Vector2 pos = transform.position;
		RaycastHit2D hit = Physics2D.Linecast (pos + dir, pos);
		return (hit.collider == GetComponent<Collider2D> ());
	}
}



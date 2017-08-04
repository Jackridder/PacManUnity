using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameBehaviour : MonoBehaviour {
	public GameObject[] lives;
	public Transform pacman;
	public AudioClip _AudioSource1;
	public AudioClip _AudioSource2;
	public static int lifeCount = 3;
	private IEnumerator coroutine;
	private AudioSource audio;
	public Text highscore;
	public GameObject pacmanDead;
	public static List<string> destroyedDots = new List<string> ();

	IEnumerator Start(){
		Debug.Log (PlayerPrefs.GetInt ("MyScore"));
		//Scene reload: which dots already eaten? Delete them!
		initDots ();
		audio = GetComponent<AudioSource>();
		//if pacman dies, livecounter decreased
		lifeCount--;
		//if player has 2 lifes: delete 1
		if (lifeCount == 1) {
			Destroy (lives [lifeCount+1]);
		}
		//if player has 1 life: delete 2
		if (lifeCount == 0) {
			Destroy (lives [lifeCount+1]);
			Destroy (lives [lifeCount+2]);
		}
		//if player has 0 lifes: Load GameOver Scene
		if (lifeCount < 0) {
			SceneManager.LoadScene ("GameOver");
		}
		//start pacman beginning theme
		audio.Play();
		//wait till beginning theme ends
		yield return new WaitForSeconds(audio.clip.length);
		//start pacman ingame music
		audio.clip = _AudioSource1;
		audio.Play();
		audio.loop = true;
	}

	public void FixedUpdate(){
		//Update Highscore 
		highscore.text = "Highscore: " + PacDot.score;
		if (PacDot.score == 329) {
			GameBehaviour.lifeCount++;
			GameBehaviour.destroyedDots = new List<string> ();
			SceneManager.LoadScene ("PacManGame");
		}
	}

	public void startLifeLost(){
		//start Deathmusic
		audio.clip = _AudioSource2;
		audio.Play ();
		audio.loop = false;
		//Start death animation
		pacmanDead.SetActive (true);
		//GhostMove.start = false;
		pacmanDead.transform.position = pacman.position;
		//wait for reloading scene
		coroutine = WaitAndLoad(audio.clip.length);
		StartCoroutine(coroutine);
	}

	private IEnumerator WaitAndLoad(float waitTime)
	{
		while (true)
		{
			yield return new WaitForSeconds(waitTime);
			SceneManager.LoadScene ("PacManGame");
		}
	}
		
	public void eatenDot(string name){
		//if pacman eat a dot: save dots name
		destroyedDots.Add(name);
	}

	private void initDots(){
		//destroy all eaten dots
		for (int i = 0; i < destroyedDots.Count; i++) {
			Destroy(GameObject.Find(destroyedDots[i]));
		}
	}
}
	
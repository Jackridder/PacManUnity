using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverBehaviour : MonoBehaviour {


	public Text highscore;
	public Text scoreText;
	IEnumerator Start()
	{
		//Set Score Text
		scoreText.text = "Neuer Score: " + PacDot.score;
		highscore.text = PlayerPrefs.GetInt ("MyScore") + "";
		//Reset some stats for next game
		GameBehaviour.lifeCount = 3;
		GameBehaviour.destroyedDots = new List<string> ();
		//Save Highscore
		GameOver ();
		PacDot.score = 0;
		//Show gameover Scene 5 seconds till Start Menu loaded
		yield return new WaitForSeconds(5);
		SceneManager.LoadScene ("StartMenu");
	}
	public void GameOver(){
		//save highscore if new highscore is greater than old highscore
		if (PlayerPrefs.GetInt ("MyScore") < PacDot.score) {
			PlayerPrefs.SetInt ("MyScore", PacDot.score);
			PlayerPrefs.Save ();
		}

	}
}


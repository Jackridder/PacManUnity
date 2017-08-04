using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour {

	//Load PacMan Game - Scene
	public void startGame(){
		SceneManager.LoadScene ("PacManGame");
	}
}

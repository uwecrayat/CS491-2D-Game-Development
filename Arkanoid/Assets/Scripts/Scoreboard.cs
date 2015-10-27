﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Scoreboard : MonoBehaviour {
	public Text currentScore;
	public Text hiscore;
    public Text lifeCount;
	private int highscore;
    public int score;
    public int lives;
    public string nextLevel;
    public string gameOverScene;

	void Awake() {
		if (!PlayerPrefs.HasKey ("highscore")) {
			PlayerPrefs.SetInt("highscore", 0);
            
		}
	}

	// Use this for initialization
	void Start () {
		score = 0;
        lives = 3;
		highscore = PlayerPrefs.GetInt ("highscore");
		currentScore.text = "" + score;
		hiscore.text = "" + highscore;
	}
	
	// Update is called once per frame
	void Update () {
        if (!GetComponent<AudioSource>().isPlaying) {
            GameObject.Find("Player").GetComponent<PlayerController>().gameStart = true;
            GameObject.Find("Ball").GetComponent<BallController>().gameStart = true;
        }
		if (score > highscore) {
			highscore = score;
			PlayerPrefs.SetInt("highscore", highscore);
		}
		currentScore.text = "" + score;
		hiscore.text = "" + PlayerPrefs.GetInt ("highscore");
        lifeCount.text = "LIVES: " + lives;

        WinLoseCondition();
	}

    void WinLoseCondition() {
        //win condition
        if (GameObject.FindGameObjectsWithTag("block").Length == 0) {
            Application.LoadLevel(nextLevel);
        }
        //lose condition
        if (lives <= 0) {
            Application.LoadLevel(gameOverScene);
        }
    }
}

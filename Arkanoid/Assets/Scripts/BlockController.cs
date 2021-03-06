﻿using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Collections;

public class BlockController : MonoBehaviour
{
	public int points;
	public GameObject[] powerups;
	private int numHitsToBreak;
	private int numHits;
	private Animator animator;

	// Use this for initialization
	void Start () {
		Random.Range (0, 6);
		numHits = 0;
		if (this.name.Contains ("steel")) {
			numHitsToBreak = 2;
			animator = GetComponent<Animator> ();
		} else {
			numHitsToBreak = 1;
		}
	}

	IEnumerator waiting () {
		yield return new WaitForSeconds (0.5f);
		animator.SetBool ("playing", false);
	}

	void OnCollisionEnter2D (Collision2D coll) {
		numHits++;
		//prevent powerups if multiball
		//don't drop powerups from steel blocks
		if (FindGameObjectsWithLayer (9).Length == 1 && !name.Contains ("steel")) {
			if (Random.Range (0, 8) == 0) {
				//if last level drop any powerup except level break
				if (Application.loadedLevel == (Application.levelCount - 2)) {
					Instantiate (powerups [Random.Range (1, 6)], transform.position, Quaternion.identity);
				} else {
					Instantiate (powerups [Random.Range (0, 6)], transform.position, Quaternion.identity);
				}
			}
		}
		if (name.Contains ("steel") && numHits < numHitsToBreak) {
			animator.SetBool ("playing", true);
			StartCoroutine (waiting ());
		}
		if (numHits >= numHitsToBreak) {
			GameObject.Find ("Canvas").GetComponent<Scoreboard> ().score += points;
			Destroy (this.gameObject);

		}
	}

	GameObject[] FindGameObjectsWithLayer (int layer) {
		GameObject[] goArray = FindObjectsOfType<GameObject> ();
		List<GameObject> goList = new List<GameObject> ();
		for (int i = 0; i < goArray.Length; i++) {
			if (goArray [i].layer == layer) {
				goList.Add (goArray [i]);
			}
		}
		if (goList.Count == 0) {
			return null;
		}
		return goList.ToArray ();
	}
}

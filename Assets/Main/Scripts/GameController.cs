﻿using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public Position playerPosition;
	public Position previousPosition; //If the player makes an invalid move, the character will snap back to previous position
	public float previousRotation = 0; // so they can turn from the previous rot
	public Position gamePosition;
	public GameObject[] sectors;

	public bool losing;
	public static int score;

	public static int x
	{
		get { return x; }
		set {x = value;}
	}

	InputManager input;

	// Use this for initialization
	void Start () {
		losing = false;
		score = 0;

		input = gameObject.GetComponent<InputManager> ();
		int i = 200; //change num tiles here to infinite
		while (i >= 0) {
			int j = Random.Range (0, sectors.Length);
			spawnSector (gamePosition.position, sectors [j]);
			i--;
		}

	}
	
	// Update is called once per frame
	void Update () {

		//User controls - take #1

		InputManager.swipes swipeDirection = InputManager.swipes.none;
		swipeDirection = input.swipe ();
		if (swipeDirection == InputManager.swipes.down) {
			previousPosition.position = playerPosition.position;
			playerPosition.position = playerPosition.Behind ();
			playerPosition.rotation = 180;

		} else if (swipeDirection == InputManager.swipes.up) {
			previousPosition.position = playerPosition.position;
			playerPosition.position = playerPosition.Forward ();
			playerPosition.rotation = 0;

		} else if (swipeDirection == InputManager.swipes.right) {
			previousPosition.position = playerPosition.position;
			playerPosition.position = playerPosition.Right ();
			playerPosition.rotation = 90;

		} else if (swipeDirection == InputManager.swipes.left) {
			previousPosition.position = playerPosition.position;
			playerPosition.position = playerPosition.Left ();
			playerPosition.rotation = 270;

		}
		//User controls - take #2

//		InputManager.swipes swipeDirection = InputManager.swipes.none;
//		swipeDirection = input.swipe ();
//		if (swipeDirection == InputManager.swipes.down) {
//				previousPosition.position = playerPosition.position;
//
//			playerPosition.rotation = 180;
//
//		} else if (swipeDirection == InputManager.swipes.up) {
//			previousPosition.position = playerPosition.position;
//
//			playerPosition.rotation = 0;
//
//		} else if (swipeDirection == InputManager.swipes.right) {
//			previousPosition.position = playerPosition.position;
//
//			playerPosition.rotation = 90;
//
//		} else if (swipeDirection == InputManager.swipes.left) {
//			previousPosition.position = playerPosition.position;
//
//			playerPosition.rotation = 270;
//
//		}
//		if (Input.touches [0].tapCount == 1) {
//			if (playerPosition.rotation == 0) {
//				previousPosition.position = playerPosition.position;
//				playerPosition.position = playerPosition.Forward ();
//			} else if (playerPosition.rotation == 90) {
//				previousPosition.position = playerPosition.position;
//				playerPosition.position = playerPosition.Right ();
//			} else if (playerPosition.rotation == 180) {
//				previousPosition.position = playerPosition.position;
//				playerPosition.position = playerPosition.Behind ();
//			} else if (playerPosition.rotation == 270) {
//				previousPosition.position = playerPosition.position;
//				playerPosition.position = playerPosition.Left ();
//			}
//		}
			
	}

	void spawnSector(Vector3 relativePosition,GameObject sector){
		GameObject newSector = GameObject.Instantiate (sector, relativePosition, transform.rotation) as GameObject;
		gamePosition.position += newSector.GetComponent<Sector> ().length;
	}
		
}

[System.Serializable]
public class Position{
	public Vector3 position;
	public float rotation;
	public Position(float x, float y, float z){
		position.x = x - (x % 1);
		position.y = y - (y % 1);
		position.z = z - (z % 1);
	}
	public Vector3 Above(){
		return new Vector3 (position.x, position.y + 1, position.z);
	}
	public Vector3 Below(){
		return new Vector3 (position.x, position.y - 1, position.z);
	}
	public Vector3 Right(){
		return new Vector3 (position.x + 1, position.y, position.z);
	}
	public Vector3 Left(){
		return new Vector3 (position.x - 1, position.y, position.z);
	}
	public Vector3 Forward(){
		return new Vector3 (position.x, position.y, position.z + 1);
	}
	public Vector3 Behind(){
		return new Vector3 (position.x, position.y, position.z - 1);
	}
	public void getBlockAtPosition(Position pos){
		
	}
}
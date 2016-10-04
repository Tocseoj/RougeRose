using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	GameController gc;

	int enemyX = 0;
	int enemyY = 0;

	// Use this for initialization
	void Start () {
		gc = GameObject.FindWithTag("GameController").GetComponent<GameController>(); Helpers.NullCheck(ref gc);
		enemyX = (int)transform.position.x; enemyY = (int)transform.position.y;
		gc.addLocation(enemyX, enemyY, transform);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AttackedByPlayer() {
		Debug.Log("Hit!");
	}
}

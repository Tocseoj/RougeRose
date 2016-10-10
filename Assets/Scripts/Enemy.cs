using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	GameController gc;
	public int sightRadius = 5;
	Transform playerTarget;

	int enemyX = 0;
	int enemyY = 0;
	int health = 100;
	const int maxHealth = 100;
	int prevTurn = 0;

	// Use this for initialization
	void Start () {
		gc = GameObject.FindWithTag("GameController").GetComponent<GameController>(); Helpers.NullCheck(ref gc);
		enemyX = (int)transform.position.x; enemyY = (int)transform.position.y;
		gc.addLocation(enemyX, enemyY, transform);
		prevTurn = gc.getTurn();

		playerTarget = GameObject.FindWithTag("Player").transform;
	}
	bool move = false;
	// Update is called once per frame
	void Update () {
		if (gc.getTurn() != prevTurn) {
			prevTurn = gc.getTurn();
			move = !move;

			if (Mathf.Abs(playerTarget.position.x - enemyX) <= 1) {
				if (Mathf.Abs(playerTarget.position.y - enemyY) <= 1) {
					move = false;
					playerTarget.SendMessage("AttackedByEnemy", 25);
				}
			}
			if (move) {
				//gc.updateLocation(ref enemyX, ref enemyY, Random.Range(-1, 2), Random.Range(-1, 2), transform);
				Vector3 towards = playerTarget.position - transform.position;
				int x = 0, y = 0;
				float ang = Vector3.Angle(Vector3.right, towards);
				if (towards.y < 0)
					ang = (-ang) + 360;
				gc.AttackPosition(ref x, ref y, ang, 1);
				gc.updateLocation(ref enemyX, ref enemyY, x, y, transform);
				transform.position = new Vector3(enemyX, enemyY, 0);
			}
		}
	}
		
	public void AttackedByPlayer(int dmg) {
		Debug.Log("Hit Enemy!");
		health -= dmg;
		if (health < 0) {
			health = 0;
			Debug.Log("Dead");
			DestroySelf();
		}
	}

	void DestroySelf() {
		gc.addLocation(enemyX, enemyY, null);
		Destroy(gameObject);
	}
}

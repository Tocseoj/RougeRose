using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	// Prefabs
	public Transform rangeIndicatorPrefab;

	// Reference Members
	GameController gc;
	SpriteRenderer rangeIndicator;

	// Player Variables
	int playerX = 0;
	int playerY = 0;
	int atkX = 0;
	int atkY = 0;
	int atkRange = 1;
	int health = 100;
	const int maxHealth = 100;

	// Use this for initialization
	void Start () {
		gc = GameObject.FindWithTag("GameController").GetComponent<GameController>(); Helpers.NullCheck(ref gc);
		playerX = (int)transform.position.x; playerY = (int)transform.position.y;
		gc.addLocation(playerX, playerY, transform);
	}
	
	// Update is called once per frame
	void Update () {

		// Movement
		if (Input.anyKeyDown) {
			int deltaX = 0, deltaY = 0;
			GetAxis(ref deltaX, ref deltaY);
			bool step = gc.updateLocation(ref playerX, ref playerY, deltaX, deltaY, transform);
			transform.position = new Vector3(playerX, playerY, 0);
			if (step)
				gc.step();
			//Debug.Log("Turn: " + gc.getTurn());
		}
		//

		// Attacking
		if (Input.GetButtonUp("Fire1")) {
			if (rangeIndicator != null)
				Destroy(rangeIndicator.gameObject);

			// TODO : dmg with SendMessage();
			Transform target = gc.getLocation((int)transform.position.x + atkX, (int)transform.position.y + atkY);
			if (target != null && target.tag != "Player") {
				//Debug.Log("hitAThing: " + target.name);	
				target.SendMessage("AttackedByPlayer", 20);
			}

			gc.step();
			atkX = 0; atkY = 0;
		}
		if (Input.GetButton("Fire1")) {
			Vector3 mouseLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mouseLocation.Set(mouseLocation.x - playerX, mouseLocation.y - playerY, 0);
			float atkAngle = Vector3.Angle(Vector3.right, mouseLocation.normalized);
			if (mouseLocation.y < 0)
				atkAngle = (-atkAngle) + 360;
			gc.AttackPosition(ref atkX, ref atkY, atkAngle, atkRange);
			if (rangeIndicator == null) {
				rangeIndicator = ((Transform)Instantiate(rangeIndicatorPrefab, transform)).GetComponent<SpriteRenderer>();
			}
			if (atkRange != 1) {
				// TODO : Change to arrow sprites
			}
			rangeIndicator.transform.localPosition = new Vector3(atkX, atkY, 0);
		}
		//
	}

	public void AttackedByEnemy(int dmg) {
		Debug.Log("Took " + dmg + " damage.");
		health -= dmg;
		if (health < 0) {
			health = 0;
			Debug.Log("Dead");
			DestroySelf();
		}
	}

	// Helper Functions
	void DestroySelf() {
		gc.addLocation(playerX, playerY, null);
		Destroy(gameObject);
	}

	void GetAxis(ref int deltaX, ref int deltaY) {
		if (Input.GetButtonDown("UpLeft")) {
			deltaX = -1; deltaY = 1;
		} else if (Input.GetButtonDown("Up")) {
			deltaX = 0; deltaY = 1;
		} else if (Input.GetButtonDown("UpRight")) {
			deltaX = 1; deltaY = 1;
		} else if (Input.GetButtonDown("Left")) {
			deltaX = -1; deltaY = 0;
		} else if (Input.GetButtonDown("Right")) {
			deltaX = 1; deltaY = 0;
		} else if (Input.GetButtonDown("DownLeft")) {
			deltaX = -1; deltaY = -1;
		} else if (Input.GetButtonDown("Down")) {
			deltaX = 0; deltaY = -1;
		} else if (Input.GetButtonDown("DownRight")) {
			deltaX = 1; deltaY = -1;
		}
	}
}

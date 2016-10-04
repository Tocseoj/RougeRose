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
			gc.updateLocation(ref playerX, ref playerY, deltaX, deltaY, transform);
			transform.position = new Vector3(playerX, playerY, 0);
		}
		//

		// Attacking
		if (Input.GetButtonUp("Fire1")) {
			if (rangeIndicator != null)
				Destroy(rangeIndicator.gameObject);

			// TODO : dmg with SendMessage();
			Transform target = gc.getLocation((int)transform.position.x + atkX, (int)transform.position.y + atkY);
			if (target != null && target.tag != "Player") {
				Debug.Log("hitAThing: " + target.name);	
				target.SendMessage("AttackedByPlayer");
			}

			atkX = 0; atkY = 0;
		}
		if (Input.GetButton("Fire1")) {
			Vector3 mouseLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mouseLocation.Set(mouseLocation.x - playerX, mouseLocation.y - playerY, 0);
			float atkAngle = Vector3.Angle(Vector3.right, mouseLocation.normalized);
			if (mouseLocation.y < 0)
				atkAngle = (-atkAngle) + 360;
			AttackPosition(ref atkX, ref atkY, atkAngle, atkRange);
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


	// Helper Functions

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

	void AttackPosition(ref int x, ref int y, float ang, int attackRange) {
		if (attackRange <= 8)
			attackRange %= 2;
		if (attackRange == 1) {
			if ((ang >= 0 && ang < 22.5f) || (ang >= 337.5f && ang < 360f)) {
				x = 1;
				y = 0;
			} else if (ang >= 22.5f && ang < 67.5f) {
				x = 1;
				y = 1;
			} else if (ang >= 67.5f && ang < 112.5f) {
				x = 0;
				y = 1;
			} else if (ang >= 112.5f && ang < 157.5f) {
				x = -1;
				y = 1;
			} else if (ang >= 157.5f && ang < 202.5f) {
				x = -1;
				y = 0;
			} else if (ang >= 202.5f && ang < 247.5f) {
				x = -1;
				y = -1;
			} else if (ang >= 247.5f && ang < 292.5f) {
				x = 0;
				y = -1;
			} else if (ang >= 292.5f && ang < 337.5f) {
				x = 1;
				y = -1;
			}
		} else if (attackRange == 0) {
			if (ang >= 0 && ang < 45) {
				x = 1;
				y = 0;
			} else if (ang >= 45 && ang < 90) {
				x = 1;
				y = 1;
			} else if (ang >= 90 && ang < 135) {
				x = 0;
				y = 1;
			} else if (ang >= 135 && ang < 180) {
				x = -1;
				y = 1;
			} else if (ang >= 180 && ang < 225) {
				x = -1;
				y = 0;
			} else if (ang >= 225 && ang < 270) {
				x = -1;
				y = -1;
			} else if (ang >= 270 && ang < 315) {
				x = 0;
				y = -1;
			} else if (ang >= 315 && ang < 360) {
				x = 1;
				y = -1;
			}
		}
	}
}
